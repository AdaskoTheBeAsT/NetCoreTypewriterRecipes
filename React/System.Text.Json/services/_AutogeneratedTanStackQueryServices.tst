${
    using System.Text;
    using System.Text.RegularExpressions;
    using Typewriter.Extensions.Types;
    using Typewriter.Extensions.WebApi;

    Template(Settings settings)
    {
        settings
            .IncludeCurrentProject()
            .UseStringLiteralCharacter('\'')
            .OutputFilenameFactory = file =>
                Hyphenated(file.Classes.First().Name.Replace("Controller", string.Empty))
                + ".queries.ts"
            ;
    }

    string Hyphenated(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var result = new StringBuilder();
        foreach (var character in value)
        {
            if (char.IsUpper(character) && result.Length > 0)
            {
                result.Append('-');
            }

            result.Append(char.ToLowerInvariant(character));
        }

        return result.ToString();
    }

    bool IncludeClass(Class c)
    {
        return c.Namespace.StartsWith("ReactWebApiSample")
            && c.Attributes.Any(p => p.Name == "GenerateFrontendType")
            && c.BaseClass != null
            && (c.BaseClass.Name.EndsWith("Controller")
                || c.BaseClass.Name.EndsWith("ControllerBase"))
            && !c.FullName.Contains("LogFileController");
    }

    bool IncludeMethod(Method method)
    {
        var httpMethod = method.HttpMethod();
        return httpMethod == "get"
            || httpMethod == "post"
            || httpMethod == "put"
            || httpMethod == "patch"
            || httpMethod == "delete";
    }

    string Quote(string value)
    {
        return "'" + (value ?? string.Empty)
            .Replace("\\", "\\\\")
            .Replace("'", "\\'")
            .Replace("\r", "\\r")
            .Replace("\n", "\\n") + "'";
    }

    string TypeName(Type type)
    {
        if (type == null)
        {
            return "unknown";
        }

        if (type.IsDictionary)
        {
            var valueType = type.TypeArguments.Count > 1 ? type.TypeArguments[1] : null;
            return valueType == null
                ? "Record<string, unknown>"
                : $"Record<string, {TypeName(valueType)}>";
        }

        if (type.IsEnumerable)
        {
            return type.ElementType == null
                ? "unknown[]"
                : $"{TypeName(type.ElementType)}[]";
        }

        if (type.IsGuid)
        {
            return "string";
        }

        if (IsDecimal(type))
        {
            return "number";
        }

        var original = type.OriginalName ?? type.Name ?? string.Empty;
        if (original.Equals("String", StringComparison.OrdinalIgnoreCase)
            || original.Equals("string", StringComparison.OrdinalIgnoreCase))
        {
            return "string";
        }

        if (original.Equals("Boolean", StringComparison.OrdinalIgnoreCase)
            || original.Equals("bool", StringComparison.OrdinalIgnoreCase))
        {
            return "boolean";
        }

        if (type.IsPrimitive && !type.IsEnum)
        {
            return "number";
        }

        return string.IsNullOrWhiteSpace(type.Name) ? "unknown" : "I" + type.Name;
    }

    string SchemaExpression(Type type)
    {
        var core = SchemaExpressionCore(type);
        return type != null && type.IsNullable
            ? $"schema.nullable({core})"
            : core;
    }

    string SchemaExpressionCore(Type type)
    {
        if (type == null)
        {
            return "schema.unknown()";
        }

        if (type.IsDictionary)
        {
            var valueType = type.TypeArguments.Count > 1 ? type.TypeArguments[1] : null;
            return valueType == null
                ? "schema.record(schema.unknown())"
                : $"schema.record({SchemaExpression(valueType)})";
        }

        if (type.IsEnumerable)
        {
            return type.ElementType == null
                ? "schema.array(schema.unknown())"
                : $"schema.array({SchemaExpression(type.ElementType)})";
        }

        if (type.IsGuid)
        {
            return "schema.uuid()";
        }

        if (IsDecimal(type))
        {
            return "schema.decimal('string')";
        }

        if (type.IsTimeSpan)
        {
            return "schema.duration()";
        }

        if (type.IsDate)
        {
            return DateSchema(type);
        }

        if (type.IsEnum)
        {
            return IsStringEnum(type) ? "schema.string()" : "schema.number()";
        }

        var original = type.OriginalName ?? type.Name ?? string.Empty;
        if (original.Equals("String", StringComparison.OrdinalIgnoreCase)
            || original.Equals("string", StringComparison.OrdinalIgnoreCase))
        {
            return "schema.string()";
        }

        if (original.Equals("Boolean", StringComparison.OrdinalIgnoreCase)
            || original.Equals("bool", StringComparison.OrdinalIgnoreCase))
        {
            return "schema.boolean()";
        }

        if (type.IsPrimitive)
        {
            return "schema.number()";
        }

        return string.IsNullOrWhiteSpace(type.Name)
            ? "schema.unknown()"
            : type.Name + "Schema";
    }

    bool IsDecimal(Type type)
    {
        var original = type?.OriginalName ?? string.Empty;
        return original.Equals("decimal", StringComparison.OrdinalIgnoreCase)
            || original.Equals("System.Decimal", StringComparison.Ordinal);
    }

    bool IsStringEnum(Type type)
    {
        return type.Attributes.Any(a => a.Name == "AsString")
            || type.Attributes.Any(a =>
                a.Name == "JsonConverter"
                && (a.Value.Contains("JsonStringEnumConverter")
                    || a.Value.Contains("StringEnumConverter")));
    }

    string DateSchema(Type type)
    {
        var original = type.OriginalName ?? string.Empty;
        return original switch
        {
            "DateTimeOffset" => "schema.instant()",
            "DateOnly" => "schema.plainDate()",
            "TimeOnly" => "schema.plainTime()",
            "Instant" => "schema.instant()",
            "LocalDate" => "schema.plainDate()",
            "LocalTime" => "schema.plainTime()",
            "LocalDateTime" => "schema.plainDateTime()",
            "ZonedDateTime" => "schema.zonedDateTime()",
            "Duration" => "schema.duration()",
            "Period" => "schema.period()",
            "YearMonth" => "schema.plainYearMonth()",
            "AnnualDate" => "schema.plainMonthDay()",
            _ => "schema.plainDateTime()",
        };
    }

    Type ResponseType(Method method)
    {
        var responseAttribute = method.Attributes
            .FirstOrDefault(attribute => attribute.Name == "ProducesResponseType");
        var responseType = responseAttribute?.Arguments
            .FirstOrDefault(argument => argument.TypeValue != null)
            ?.TypeValue;
        if (responseType != null)
        {
            return responseType;
        }

        var methodType = method.Type;
        if (methodType == null
            || methodType.Name == "IActionResult"
            || methodType.Name == "ActionResult")
        {
            return null;
        }

        return methodType.Unwrap();
    }

    List<Parameter> Parameters(Method method)
    {
        return method.Parameters
            .Where(parameter => parameter.Type.Name != "CancellationToken")
            .ToList();
    }

    Parameter BodyParameter(Method method)
    {
        var parameters = Parameters(method);
        var explicitBody = parameters.FirstOrDefault(
            parameter => parameter.Attributes.Any(attribute => attribute.Name == "FromBody"));
        if (explicitBody != null)
        {
            return explicitBody;
        }

        var httpMethod = method.HttpMethod();
        if (httpMethod == "get" || httpMethod == "delete")
        {
            return null;
        }

        return parameters.FirstOrDefault(parameter => !IsRouteParameter(method, parameter));
    }

    bool IsRouteParameter(Method method, Parameter parameter)
    {
        return parameter.Attributes.Any(attribute => attribute.Name == "FromRoute")
            || MethodRoute(method).Contains("{" + parameter.Name + "}", StringComparison.OrdinalIgnoreCase);
    }

    List<Parameter> QueryParameters(Method method)
    {
        var body = BodyParameter(method);
        return Parameters(method)
            .Where(parameter => parameter != body && !IsRouteParameter(method, parameter))
            .ToList();
    }

    string AttributeString(Attribute attribute)
    {
        var argument = attribute?.Arguments.FirstOrDefault(
            candidate => string.IsNullOrEmpty(candidate.Name));
        if (argument?.Value != null)
        {
            return argument.Value.ToString();
        }

        var match = Regex.Match(attribute?.Value ?? string.Empty, @"""([^""]*)""");
        return match.Success ? match.Groups[1].Value : string.Empty;
    }

    string ControllerRoute(Class c)
    {
        var route = AttributeString(c.Attributes.FirstOrDefault(attribute => attribute.Name == "Route"));
        return route.Replace("[controller]", c.Name.Replace("Controller", string.Empty));
    }

    string MethodRoute(Method method)
    {
        var httpMethod = method.HttpMethod();
        var attributeName = httpMethod switch
        {
            "get" => "HttpGet",
            "post" => "HttpPost",
            "put" => "HttpPut",
            "patch" => "HttpPatch",
            "delete" => "HttpDelete",
            _ => string.Empty,
        };
        return AttributeString(
            method.Attributes.FirstOrDefault(attribute => attribute.Name == attributeName));
    }

    string MethodName(Method method)
    {
        var customName = AttributeString(
            method.Attributes.FirstOrDefault(attribute => attribute.Name == "CustomName"));
        var name = string.IsNullOrWhiteSpace(customName) ? method.Name : customName;
        foreach (var parameter in Parameters(method))
        {
            name += TypeToken(parameter.Type);
        }

        return char.ToUpperInvariant(name[0]) + name.Substring(1);
    }

    string TypeToken(Type type)
    {
        if (type == null)
        {
            return "Unknown";
        }

        if (type.IsDictionary)
        {
            return "Dictionary";
        }

        if (type.IsEnumerable)
        {
            return "Array" + TypeToken(type.ElementType);
        }

        var value = type.OriginalName ?? type.Name ?? "Unknown";
        var lastDot = value.LastIndexOf('.');
        if (lastDot >= 0)
        {
            value = value.Substring(lastDot + 1);
        }

        value = Regex.Replace(value, @"[^A-Za-z0-9_]", string.Empty);
        return string.IsNullOrWhiteSpace(value)
            ? "Unknown"
            : char.ToUpperInvariant(value[0]) + value.Substring(1);
    }

    string HookName(Method method)
    {
        return "use" + MethodName(method)
            + (method.HttpMethod() == "get" ? "Query" : "Mutation");
    }

    string MethodParameters(Method method)
    {
        return string.Join(
            ", ",
            Parameters(method).Select(parameter => $"{parameter.name}: {TypeName(parameter.Type)}"));
    }

    string MutationType(Method method)
    {
        var parameters = Parameters(method);
        if (parameters.Count == 0)
        {
            return "void";
        }

        if (parameters.Count == 1)
        {
            return TypeName(parameters[0].Type);
        }

        return "{ "
            + string.Join("; ", parameters.Select(parameter => $"{parameter.name}: {TypeName(parameter.Type)}"))
            + " }";
    }

    string MutationParameter(Method method)
    {
        var parameters = Parameters(method);
        if (parameters.Count == 0)
        {
            return string.Empty;
        }

        if (parameters.Count == 1)
        {
            return $"{parameters[0].name}: {TypeName(parameters[0].Type)}";
        }

        return "{ "
            + string.Join(", ", parameters.Select(parameter => parameter.name))
            + " }: "
            + MutationType(method);
    }

    string UrlStatements(Method method, string indent)
    {
        var route = MethodRoute(method);
        var urlExpression = "controllerUrl";
        if (!string.IsNullOrWhiteSpace(route))
        {
            var routeExpression = Quote("/" + route.TrimStart('/'));
            foreach (var parameter in Parameters(method).Where(parameter => IsRouteParameter(method, parameter)))
            {
                var openBrace = ((char)123).ToString();
                var closeBrace = ((char)125).ToString();
                var placeholderMatch = Regex.Match(
                    route,
                    Regex.Escape(openBrace)
                        + Regex.Escape(parameter.Name)
                        + "(?::[^"
                        + Regex.Escape(closeBrace)
                        + "]+)?"
                        + Regex.Escape(closeBrace),
                    RegexOptions.IgnoreCase);
                if (placeholderMatch.Success)
                {
                    routeExpression += ".replace("
                        + Quote(placeholderMatch.Value)
                        + ", encodeURIComponent(String("
                        + parameter.name
                        + ")))";
                }
            }

            urlExpression += " + " + routeExpression;
        }

        var queryParameters = QueryParameters(method);
        if (queryParameters.Count == 0)
        {
            return indent + "const url = " + urlExpression + ";";
        }

        var queryValue = queryParameters.Count == 1
            && !queryParameters[0].Type.IsPrimitive
            && !queryParameters[0].Type.IsEnum
            && !queryParameters[0].Type.IsGuid
            && !queryParameters[0].Type.IsDate
                ? queryParameters[0].name
                : "{ " + string.Join(", ", queryParameters.Select(parameter => parameter.name)) + " }";

        return indent + "const queryString = qs.stringify(" + queryValue + ");\n"
            + indent + "const baseUrl = " + urlExpression + ";\n"
            + indent + "const url = queryString ? baseUrl + '?' + queryString : baseUrl;";
    }

    string RequestInit(Method method, string indent)
    {
        var httpMethod = method.HttpMethod().ToUpperInvariant();
        var body = BodyParameter(method);
        if (body == null)
        {
            return "{\n"
                + indent + "  method: " + Quote(httpMethod) + ",\n"
                + indent + "  headers: { Accept: 'application/json', 'If-Modified-Since': '0' },\n"
                + indent + "}";
        }

        return "{\n"
            + indent + "  method: " + Quote(httpMethod) + ",\n"
            + indent + "  headers: { Accept: 'application/json', 'Content-Type': 'application/json', 'If-Modified-Since': '0' },\n"
            + indent + "  body: serializeJsonBody(" + body.name + ", " + SchemaExpression(body.Type) + ", { registry: apiTypeRegistry }),\n"
            + indent + "}";
    }

    string RenderMethod(Method method)
    {
        if (!IncludeMethod(method))
        {
            return string.Empty;
        }

        var responseType = ResponseType(method);
        var hasResponse = responseType != null;
        var responseName = hasResponse ? TypeName(responseType) : "void";
        var responseSchema = hasResponse ? SchemaExpression(responseType) : string.Empty;
        var hookName = HookName(method);
        var methodKey = Quote(((Class)method.Parent).Name + "." + MethodName(method));
        var queryKeyArguments = string.Join(
            ", ",
            Parameters(method).Select(parameter => parameter.name));

        if (method.HttpMethod() == "get")
        {
            var keySuffix = string.IsNullOrEmpty(queryKeyArguments)
                ? string.Empty
                : ", " + queryKeyArguments;
            return "export function " + hookName + "(" + MethodParameters(method) + ") {\n"
                + "  return useQuery<" + responseName + ">({\n"
                + "    queryKey: [" + methodKey + keySuffix + "],\n"
                + "    queryFn: () => {\n"
                + UrlStatements(method, "      ") + "\n"
                + "      return fetchJson<" + responseName + ">(url, " + responseSchema + ", undefined, {\n"
                + "        registry: apiTypeRegistry,\n"
                + "      });\n"
                + "    },\n"
                + "  });\n"
                + "}";
        }

        var mutationParameter = MutationParameter(method);
        var mutationSignature = string.IsNullOrEmpty(mutationParameter)
            ? "()"
            : "(" + mutationParameter + ")";
        var requestInit = RequestInit(method, "      ");
        var request = hasResponse
            ? "return fetchJson<" + responseName + ">(url, " + responseSchema + ", " + requestInit + ", {\n"
                + "        registry: apiTypeRegistry,\n"
                + "      });"
            : "const response = await fetch(url, " + requestInit + ");\n"
                + "      if (!response.ok) {\n"
                + "        throw new Error(('HTTP request failed with status ' + response.status + ' ' + response.statusText).trim());\n"
                + "      }";

        return "export function " + hookName + "() {\n"
            + "  return useMutation<" + responseName + ", Error, " + MutationType(method) + ">({\n"
            + "    mutationFn: async " + mutationSignature + " => {\n"
            + UrlStatements(method, "      ") + "\n"
            + "      " + request.Replace("\n", "\n      ") + "\n"
            + "    },\n"
            + "  });\n"
            + "}";
    }

    string Hooks(Class c)
    {
        return string.Join(
            "\n\n",
            c.Methods.Where(IncludeMethod).Select(RenderMethod));
    }

    string Imports(Class c)
    {
        var modelTypes = new SortedDictionary<string, string>(StringComparer.Ordinal);
        var schemaTypes = new SortedDictionary<string, string>(StringComparer.Ordinal);
        foreach (var method in c.Methods.Where(IncludeMethod))
        {
            AddModelType(ResponseType(method), modelTypes);
            AddModelType(ResponseType(method), schemaTypes);
            AddModelType(BodyParameter(method)?.Type, schemaTypes);
            foreach (var parameter in Parameters(method))
            {
                AddModelType(parameter.Type, modelTypes);
            }
        }

        var lines = new List<string>();
        foreach (var pair in modelTypes)
        {
            lines.Add($"import type {{ I{pair.Key} }} from '../models/{pair.Key}';");
            if (schemaTypes.ContainsKey(pair.Key))
            {
                lines.Add($"import {{ {pair.Key}Schema }} from '../models/{pair.Key}.schema';");
            }
        }

        return string.Join("\n", lines);
    }

    void AddModelType(Type type, IDictionary<string, string> modelTypes)
    {
        if (type == null)
        {
            return;
        }

        if (type.IsDictionary)
        {
            if (type.TypeArguments.Count > 1)
            {
                AddModelType(type.TypeArguments[1], modelTypes);
            }
            return;
        }

        if (type.IsEnumerable)
        {
            AddModelType(type.ElementType, modelTypes);
            return;
        }

        if (type.IsPrimitive
            || type.IsEnum
            || type.IsDate
            || type.IsGuid
            || type.IsTimeSpan
            || IsDecimal(type)
            || string.IsNullOrWhiteSpace(type.Name))
        {
            return;
        }

        modelTypes[type.Name] = type.Name;
    }

    bool UsesQueryString(Class c)
    {
        return c.Methods.Where(IncludeMethod).Any(method => QueryParameters(method).Count > 0);
    }

    bool UsesInlineSchema(Class c)
    {
        foreach (var method in c.Methods.Where(IncludeMethod))
        {
            if (UsesInlineSchema(ResponseType(method))
                || UsesInlineSchema(BodyParameter(method)?.Type))
            {
                return true;
            }
        }

        return false;
    }

    bool UsesInlineSchema(Type type)
    {
        if (type == null)
        {
            return false;
        }

        return type.IsDictionary
            || type.IsEnumerable
            || type.IsPrimitive
            || type.IsEnum
            || type.IsDate
            || type.IsGuid
            || type.IsTimeSpan
            || IsDecimal(type)
            || string.IsNullOrWhiteSpace(type.Name);
    }
}
$Classes($IncludeClass)[
// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.
import { useMutation, useQuery } from '@tanstack/react-query';
import { fetchJson, serializeJsonBody } from '@adaskothebeast/typewriter-http-fetch';
$UsesInlineSchema[import { schema } from '@adaskothebeast/typewriter-schema';
]$UsesQueryString[import * as qs from 'qs';
]import { environment } from '../../environments/environment';
import { apiTypeRegistry } from '../models/typewriter-registry';
$Imports

const controllerUrl = `${environment.apiBaseUrl.replace(/\/$/, '')}/$ControllerRoute`;

$Hooks
]
