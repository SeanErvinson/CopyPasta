<template>
	<CThemeProvider>
		<CColorModeProvider>
			<CBox font-family="body" as="main" h="100%" w="100%">
				<CReset />
				<Header />
				<Nuxt />
				<Footer />
			</CBox>
		</CColorModeProvider>
	</CThemeProvider>
</template>

<script lang="ts">
import Vue from 'vue'
import Header from '../components/layouts/Header.vue'
import Footer from '../components/layouts/Footer.vue'
import { CThemeProvider, CColorModeProvider, CReset, CBox } from '@chakra-ui/vue'

export default Vue.extend({
	components: {
		Header,
		Footer,
		CThemeProvider,
		CColorModeProvider,
		CReset,
		CBox,
	},
})
</script>

<style>
html {
	font-family: 'Helvetica Neue', 'Segoe UI', Arial, sans-serif;
	font-size: 16px;
	word-spacing: 1px;
	-ms-text-size-adjust: 100%;
	-webkit-text-size-adjust: 100%;
	-moz-osx-font-smoothing: grayscale;
	-webkit-font-smoothing: antialiased;
	box-sizing: border-box;
}

*,
*::before,
*::after {
	box-sizing: border-box;
	margin: 0;
}
html,
body {
	height: 100%;
	background: #f3f4f6;
}
div#__nuxt,
#__layout,
#__layout > div,
#app {
	height: 100vh;
}
main {
	display: flex;
	flex-direction: column;
}
main > div {
	flex: 1;
}
.modal > section {
	margin: 8px;
	border-radius: 8px;
}
</style>
