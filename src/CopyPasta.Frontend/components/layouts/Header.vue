<template>
	<c-box as="header" d="flex" flexDirection="column" mx="auto" mb="2" align-items="center">
		<c-heading as="h1" size="2xl" color="gray.800" fontWeight="semibold">
			<c-link as="router-link" to="/"> CopyPasta </c-link>
		</c-heading>
		<c-heading as="u" size="lg" color="gray.500" fontWeight="medium">
			CopyPasta just
			<c-box as="span" bg="gray.500" px="1" color="white" borderRadius="md">Ctrl+C</c-box>
			<c-box as="span" bg="gray.500" px="1" color="white" borderRadius="md">Ctrl+V</c-box>
		</c-heading>
		<h2 class="font-medium text-gray-500 underline text-2xl md:text-3xl"></h2>
	</c-box>
</template>

<script>
import { CHeading, CBox } from '@chakra-ui/vue'
export default {
	components: {
		CHeading,
		CBox,
	},
}
</script>

<style></style>
