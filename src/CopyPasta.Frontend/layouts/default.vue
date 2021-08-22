<template>
	<div class="flex flex-col gap-y-2">
		<Header />
		<Nuxt class="flex-grow" />
		<h1>Footer</h1>
	</div>
</template>

<script lang="ts">
import Vue from 'vue'
import Header from '../components/layouts/Header.vue'

export default Vue.extend({
	components: {
		Header,
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
</style>
