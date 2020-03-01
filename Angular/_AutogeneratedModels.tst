${
    using Typewriter.Extensions.Types;
    
    Template(Settings settings)
    {
        settings
            .IncludeCurrentProject()
            .IncludeReferencedProjects()
            ;
    }

    bool IncludeClass(Class c){
        if(!c.Namespace.StartsWith("AngularWebApiSample"))
        {
            return false;
        }

        var attr = c.Attributes.FirstOrDefault(p => p.Name == "GenerateFrontendType");
        if(attr == null){
            return false;
        }

        var parent = c.BaseClass;
        if(parent != null){
            if(parent.Name.EndsWith("Controller")
          || parent.Name.EndsWith("ControllerBase"))
          {
            return false;
          }
        }        

        return true;
    }

    bool IncludeEnums(Enum e){
        if(!e.Namespace.StartsWith("AngularWebApiSample"))
        {
            return false;
        }

        var attr = e.Attributes.FirstOrDefault(p => p.Name == "GenerateFrontendType");
        if(attr == null){
            return false;
        }

        return true;
    }

    string Imports(Class c)
    {
        var neededImports = c.Properties
	        .Where(p => (!p.Type.IsPrimitive || p.Type.IsEnum) && (p.Type.ClassName() != "any" && p.Type.ClassName() != "T") && IncludeProperty(p))
	        .Select(p => $"import {{ {p.Type.ClassName()} }} from './{p.Type.ClassName()}';").ToList();
    
        if(c.BaseClass != null && c.BaseClass.TypeArguments != null)
        {
            foreach(var typeArgument in c.BaseClass.TypeArguments)
            {
                neededImports.Add($"import {{ I{typeArgument.Name}, {typeArgument.Name} }} from './{typeArgument.Name}';");
            }
        }

        if(c.BaseClass != null)
        {
            neededImports.Add($"import {{ I{c.BaseClass.ToString()}, {c.BaseClass.ToString()} }} from './{c.BaseClass.ToString()}';");
        }

        return String.Join(Environment.NewLine, neededImports.Distinct());
    }

    string InheritClass(Class c)
    {
        if(c.BaseClass != null)
        {
            if(c.BaseClass.IsGeneric)
            {
                return $" extends {c.BaseClass.ToString()}<{c.BaseClass.TypeArguments.First()}>";
            }
            else
            {
                return $" extends {c.BaseClass.ToString()}";
            }
        }
        else
        {
            return string.Empty;
        }
    }

    string InheritInterface(Class c)
    {
        if(c.BaseClass != null)
        {
            if(c.BaseClass.IsGeneric)
            {
                return $" extends I{c.BaseClass.ToString()}<{c.BaseClass.TypeArguments.First()}>";
            }
            else
            {
                return $" extends I{c.BaseClass.ToString()}";
            }
        }
        else
        {
            return string.Empty;
        }
    }

    string ImplementsInterface(Class c)
    {
        if(c.IsGeneric)
        {
            return $" implements I{c.ToString()}<{c.TypeArguments.First()}>";
        }
        else
        {
            return $" implements I{c.ToString()}";
        }
    }

    string Super(Class c){
        if(c.BaseClass == null)
        {
            return string.Empty;
        }
        return $"{Environment.NewLine}        super(initObj);";
    }

    string GenerateTypeForInterface(Class c){
        return c.BaseClass == null ? $"{Environment.NewLine}    $type?: string;" : string.Empty;
    }

    string GenerateTypeForClass(Class c){
        return c.BaseClass == null ? $"{Environment.NewLine}    public $type: string;" : string.Empty;
    }

    string GenerateTypeInit(Class c){
        var dllName = c.Namespace;
        var attr = c.Attributes.FirstOrDefault(p => p.Name == "GenerateFrontendType");
        if(attr != null && !string.IsNullOrEmpty(attr.Value)) {
            dllName = attr.Value;
        }
        return $"this.$type = '{c.FullName},'\r\n            + '{dllName}';";
    }

    string SimplifyType(Property property){
        return property.Type.Name;
    }

    string NullableMark(Property property) {
      return property.Type.IsNullable ? "?" : string.Empty;
    }

    string ReturnTypeDefault(Type type) {
        return type.Default().Replace("\"", $"{(char)39}");
    }

    string GetAttributeValueOrReturnEnumNameIfNoAttribute(EnumValue enumObj) {
        if (enumObj.Attributes.Any(a=>a.Name=="LabelForEnum")) {
            return enumObj.Attributes.First(a=>a.Name=="LabelForEnum").Value;
        } else {
            return enumObj.Name;
        }
    }

    bool IsEnumAsNumber(Enum e) {
      if(e.Attributes.Any(a=>a.Name=="AsString")){
        return false;
      }
      return true;
    }

    string GetEnumAsStringIfItsStringable(EnumValue enumObj) {
        var parent = (enumObj.Parent as Enum);
        if(parent.Attributes.Any(a=>a.Name=="AsString")){
            return "'"+enumObj.Name+"'";
        } else {
            return enumObj.Value.ToString();
        }
    }

    bool IncludeProperty(Property property) {
        var attr = property.Attributes.FirstOrDefault(p => p.Name == "JsonIgnore");
        if(attr != null){
            return false;
        }
        return true;
    }
}// This file has been AUTOGENERATED by TypeWriter (https://frhagn.github.io/Typewriter/).
// Do not modify it.
$Enums($IncludeEnums)[
export enum $Name {$Values[
    $Name = $GetEnumAsStringIfItsStringable][,
    ]
}

export namespace $Name {
    export function getLabel(value: $Name): string {
        var toReturn = '';
        switch(value) {$Values[
            case $Parent[$Name].$Name: toReturn = '$GetAttributeValueOrReturnEnumNameIfNoAttribute'; break;][
            ]
        }
        return toReturn;
    } $IsEnumAsNumber[
    export function getKeys(): Array<number> {
        var list = new Array<number>();
        for (var enumMember in $Name) { 
            if (!(parseInt(enumMember, 10) >= 0)) {
                  continue;
            }
            list.push(parseInt(enumMember, 10));
        }

        return list;     
    }]
}
]
$Classes($IncludeClass)[
$Imports

export interface I$Name$TypeParameters$InheritInterface {$GenerateTypeForInterface$Properties($IncludeProperty)[
    $name?: $SimplifyType;]
}

export class $Name$TypeParameters$InheritClass$ImplementsInterface {$GenerateTypeForClass$Properties($IncludeProperty)[
    public $name$NullableMark: $SimplifyType;]

    constructor(initObj?: I$Name$TypeParameters) {$Super
        $GenerateTypeInit
        if (initObj) {$Properties($IncludeProperty)[
            this.$name = initObj.$name || $Type[$ReturnTypeDefault];]
        } else {$Properties($IncludeProperty)[
            this.$name = $Type[$ReturnTypeDefault];]
        }
    }
}]
