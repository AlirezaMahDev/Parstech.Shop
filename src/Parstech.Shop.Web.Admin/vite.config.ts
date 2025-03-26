import {UserConfig} from "vite";
import {resolve} from 'node:path';
import tailwindcss from '@tailwindcss/vite'

export default {
    build: {
        cssCodeSplit: true,
        lib: {
            entry: {
                app: resolve(__dirname, 'source/app/index.ts'),
            },
            name: 'parstech',
        },
        outDir: resolve(__dirname, 'wwwroot/lib'),
        sourcemap: "inline"
    },
    plugins: [
        tailwindcss(),
    ]
} satisfies UserConfig