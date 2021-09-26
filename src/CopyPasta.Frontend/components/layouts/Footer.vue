<template>
	<c-box as="footer" d="flex" flexDirection="column" mx="auto" mb="2" align-items="center">
		<c-box>
			<c-link as="router-link" to="/"> 🏠 Home </c-link> •
			<c-link as="router-link" to="/terms"> 📑 Terms </c-link>
		</c-box>
		<c-text fontSize="sm">SeanErvinson - Yoink</c-text>
	</c-box>
</template>

<script lang="ts">
import Vue from 'vue'
import { CText, CBox } from '@chakra-ui/vue'

export default Vue.extend({
	components: { CText, CBox },
})
</script>

<style></style>
