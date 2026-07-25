import { FlatCompat } from "@eslint/eslintrc";
import { dirname } from "path";
import { fileURLToPath } from "url";
import js from "@eslint/js";
import nx from "@nx/eslint-plugin";
import eslintPluginTestcafe from "eslint-plugin-testcafe";

const compat = new FlatCompat({
  baseDirectory: dirname(fileURLToPath(import.meta.url)),
  recommendedConfig: js.configs.recommended,
});


export default [
    ...compat.extends("plugin:testcafe/recommended"),
    ...nx.configs["flat/base"],
    { plugins: { "testcafe": eslintPluginTestcafe } },
    {
        files: [
            "**/*.ts",
            "**/*.tsx",
            "**/*.js",
            "**/*.jsx"
        ],
        rules: {
            "@nx/enforce-module-boundaries": [
                "error",
                {
                    enforceBuildableLibDependency: true,
                    allow: [],
                    depConstraints: [
                        {
                            sourceTag: "*",
                            onlyDependOnLibsWithTags: [
                                "*"
                            ]
                        }
                    ]
                }
            ]
        }
    },
    ...nx.configs["flat/typescript"],
    ...nx.configs["flat/javascript"],
    ...compat.config({
        env: {
            jest: true
        }
    }).map(config => ({
        ...config,
        files: [
            "**/*.spec.ts",
            "**/*.spec.tsx",
            "**/*.spec.js",
            "**/*.spec.jsx"
        ],
        rules: {
            ...config.rules
        }
    })),
    {
        ignores: [
            "**/jest/**/*.ts",
            "**/node_modules/**",
            "**/src/api/**/*.ts",
            "**/*eslintrc.js",
            "./resultsProcessor.js",
            "jest.config.js"
        ]
    }
];
