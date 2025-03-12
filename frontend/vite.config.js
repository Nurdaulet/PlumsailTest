import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
  plugins: [vue()], // Add Vue support
  build: {
    rollupOptions: {
      output: {
        entryFileNames: 'assets/bundle.js',  // JS bundle output
        assetFileNames: 'assets/bundle.[ext]', // CSS and other assets
      },
    },
  },
});
