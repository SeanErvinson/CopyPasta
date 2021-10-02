import mainTheme from './ui/main-theme.js'

export default {
	ssr: false,

	target: 'static',

	head: {
		title: 'CopyPasta.Frontend',
		htmlAttrs: {
			lang: 'en',
		},
		meta: [
			{ charset: 'utf-8' },
			{ name: 'viewport', content: 'width=device-width, initial-scale=1' },
			{ hid: 'description', name: 'description', content: '' },
			{ name: 'format-detection', content: 'telephone=no' },
		],
		link: [{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }],
	},

	css: [],

	plugins: ['~/plugins/chakra'],

	components: true,

	buildModules: ['@nuxt/typescript-build'],

	modules: ['@nuxtjs/axios', '@chakra-ui/nuxt'],

	axios: {
		baseUrl: 'https://localhost:5001/api',
	},

	publicRuntimeConfig: {
		axios: {
			baseUrl: process.env.BASE_URL || 'https://localhost:5001/api',
		},
	},

	build: {},
	chakra: {
		extendTheme: mainTheme,
	},
}
