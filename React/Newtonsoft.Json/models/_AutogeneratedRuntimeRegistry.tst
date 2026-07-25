${
    using Typewriter.Extensions.Types;

    private Class[] includedClasses = Array.Empty<Class>();
    private Record[] includedRecords = Array.Empty<Record>();

    Template(Settings settings, File file)
    {
        settings
            .IncludeCurrentProject()
            .IncludeReferencedProjects()
            .UseStringLiteralCharacter('\'')
            .SingleFileMode("typewriter-registry.ts")
            ;

        includedClasses = file.Classes.Where(IncludeClass).ToArray();
        includedRecords = file.Records.Where(IncludeRecord).ToArray();
    }

    bool IsRecipeNamespace(string value)
    {
        return value.StartsWith("AngularWebApiSample")
            || value.StartsWith("ReactWebApiSample");
    }

    bool IncludeClass(Class c)
    {
        if (!IsRecipeNamespace(c.Namespace)
            || !c.Attributes.Any(p => p.Name == "GenerateFrontendType")
            || c.IsGeneric)
        {
            return false;
        }

        var parent = c.BaseClass;
        return parent == null
            || (!parent.Name.EndsWith("Controller")
                && !parent.Name.EndsWith("ControllerBase"));
    }

    bool IncludeRecord(Record r)
    {
        if (!IsRecipeNamespace(r.Namespace)
            || !r.Attributes.Any(p => p.Name == "GenerateFrontendType")
            || r.IsGeneric)
        {
            return false;
        }

        var parent = r.BaseRecord;
        return parent == null
            || (!parent.Name.EndsWith("Controller")
                && !parent.Name.EndsWith("ControllerBase"));
    }

    string SchemaName(Class c) => c.Name + "Schema";
    string SchemaName(Record r) => r.Name + "Schema";

    string Quote(string value)
    {
        return "'" + (value ?? string.Empty)
            .Replace("\\", "\\\\")
            .Replace("'", "\\'")
            .Replace("\r", "\\r")
            .Replace("\n", "\\n") + "'";
    }

    bool DerivesFrom(Class candidate, Class baseClass)
    {
        var parent = candidate.BaseClass;
        while (parent != null)
        {
            if (parent.FullName == baseClass.FullName
                && parent.AssemblyName == baseClass.AssemblyName)
            {
                return true;
            }

            parent = parent.BaseClass;
        }

        return false;
    }

    bool DerivesFrom(Record candidate, Record baseRecord)
    {
        var parent = candidate.BaseRecord;
        while (parent != null)
        {
            if (parent.FullName == baseRecord.FullName
                && parent.AssemblyName == baseRecord.AssemblyName)
            {
                return true;
            }

            parent = parent.BaseRecord;
        }

        return false;
    }

    IEnumerable<string> DiscriminatorValues(Class c)
    {
        yield return c.FullName + "," + c.AssemblyName;
        yield return c.FullName + ", " + c.AssemblyName;
    }

    IEnumerable<string> DiscriminatorValues(Record r)
    {
        yield return r.FullName + "," + r.AssemblyName;
        yield return r.FullName + ", " + r.AssemblyName;
    }

    string RegistryDefinition(Class c)
    {
        var derived = includedClasses
            .Where(candidate => candidate.FullName != c.FullName && DerivesFrom(candidate, c))
            .OrderBy(candidate => candidate.FullName, StringComparer.Ordinal)
            .ToArray();
        if (derived.Length == 0)
        {
            return SchemaName(c);
        }

        var variants = new SortedDictionary<string, string>(StringComparer.Ordinal);
        foreach (var candidate in new[] { c }.Concat(derived))
        {
            foreach (var discriminator in DiscriminatorValues(candidate))
            {
                variants[discriminator] =
                    $"({SchemaName(candidate)} as RuntimeSchema<unknown>)";
            }
        }

        var entries = string.Join(
            ",\n",
            variants.Select(pair => $"    {Quote(pair.Key)}: {pair.Value}"));
        return $"() => schema.discriminatedUnion<unknown>('$type', {{\n{entries}\n  }})";
    }

    string RegistryDefinition(Record r)
    {
        var derived = includedRecords
            .Where(candidate => candidate.FullName != r.FullName && DerivesFrom(candidate, r))
            .OrderBy(candidate => candidate.FullName, StringComparer.Ordinal)
            .ToArray();
        if (derived.Length == 0)
        {
            return SchemaName(r);
        }

        var variants = new SortedDictionary<string, string>(StringComparer.Ordinal);
        foreach (var candidate in new[] { r }.Concat(derived))
        {
            foreach (var discriminator in DiscriminatorValues(candidate))
            {
                variants[discriminator] =
                    $"({SchemaName(candidate)} as RuntimeSchema<unknown>)";
            }
        }

        var entries = string.Join(
            ",\n",
            variants.Select(pair => $"    {Quote(pair.Key)}: {pair.Value}"));
        return $"() => schema.discriminatedUnion<unknown>('$type', {{\n{entries}\n  }})";
    }
}
// This file has been AUTOGENERATED by TypeWriter (https://github.com/adaskothebeast/Typewriter).
// Do not modify it.
import { defineTypeRegistry, schema } from '@adaskothebeast/typewriter-schema';
import type { RuntimeSchema } from '@adaskothebeast/typewriter-schema';
$Classes($IncludeClass)[
import { $SchemaName } from './$Name.schema';
]$Records($IncludeRecord)[
import { $SchemaName } from './$Name.schema';
]

export const apiTypeRegistry = defineTypeRegistry({
$Classes($IncludeClass)[  '$FullName': $RegistryDefinition,
]$Records($IncludeRecord)[  '$FullName': $RegistryDefinition,
]});
