"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const tsup_1 = require("tsup");
exports.default = (0, tsup_1.defineConfig)({
    format: ["cjs", "esm"],
    // The file we created above that will be the entrypoint to the library.
    entry: ["src/index.tsx"],
    // Enable TypeScript type definitions to be generated in the output.
    // This provides type-definitions to consumers.
    dts: true,
    // Clean the `dist` directory before building.
    // This is useful to ensure the output is only the latest.
    clean: true,
    // Sourcemaps for easier debugging.
    sourcemap: true,
});
