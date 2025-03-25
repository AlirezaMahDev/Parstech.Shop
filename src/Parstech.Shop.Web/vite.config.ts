import {UserConfig} from "vite";
import {resolve} from 'node:path';
import tailwindcss from '@tailwindcss/vite'

export default {
    build: {
        cssCodeSplit: true,
        lib: {
            entry: {
                script: resolve(__dirname, 'source/scripts/index.ts'),
                style: resolve(__dirname, 'source/styles/index.css'),
            },
            name: 'parstech',
        },
        outDir: resolve(__dirname, 'wwwroot/lib'),
        sourcemap: "inline"
    },
    plugins: [
        tailwindcss(),
    ],
} satisfies UserConfig