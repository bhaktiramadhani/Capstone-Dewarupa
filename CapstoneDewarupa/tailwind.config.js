/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Pages/**/*.{cshtml,html}",
    "./Views/**/*.{cshtml,html}",
    "./wwwroot/**/*.html",
    "./node_modules/flowbite/**/*.js",
    "./Areas/Identity/Pages/**/*.{cshtml,html}",
  ],
  theme: {
    extend: {
      colors: {
        "primary-green": "#20B486",
        "primary-green-hover": "#53C7A3",
      },
      boxShadow: {
        card: "5px 20px 50px 0px rgba(0, 0, 0, 0.20)",
        "card-hover": "5px 20px 50px 0px rgba(0, 0, 0, 0.40)",
        "kontak-card": "2px 4px 10px 4px rgba(0, 0, 0, 0.10)",
      },
    },
  },
  plugins: [require("flowbite/plugin"), require("@tailwindcss/typography")],
};
