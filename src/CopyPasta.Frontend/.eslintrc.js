module.exports = {
	root: true,
	env: {
		browser: true,
		node: true,
	},
	extends: ['@nuxtjs/eslint-config-typescript', 'plugin:nuxt/recommended', 'prettier'],
	plugins: [],
	rules: {
		'vue/component-definition-name-casing': ['error', 'kebab-case'],
		'vue/attribute-hyphenation': ['error', 'always'],
		'vue/order-in-components': ['error'],
	},
}
