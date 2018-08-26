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
        return c.Name.EndsWith("Model");
    }

    bool IncludeEnums(Enum e){
        return e.Namespace.Contains("Models");
    }

    string Imports(Class c)
    {
        var neededImports = c.Properties
            .Where(p => !p.Type.IsPrimitive)
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
        var pos = c.FullName.IndexOf(".Models");
       
        return $"this.$type = '{c.FullName}, {c.FullName.Substring(0, pos)}';";
    }

    string SimplifyType(Property property){
        return property.Type.Name;
    }
}
$Enums($IncludeEnums)[
export enum $Name {$Values[
        $Name = "$Name"][,
    ]
}    
]
$Classes($IncludeClass)[$Imports

export interface I$Name$TypeParameters$InheritInterface {$GenerateTypeForInterface$Properties[
        $name?: $SimplifyType;]
}

export class $Name$TypeParameters$InheritClass$ImplementsInterface {$GenerateTypeForClass$Properties[
        public $name: $SimplifyType;]

    constructor(initObj?: I$Name$TypeParameters) {$Super
        $GenerateTypeInit
        if (initObj) {$Properties[
            this.$name = initObj.$name || $Type[$Default];]
        } else {$Properties[
            this.$name = $Type[$Default];]
        }
    }
}]
