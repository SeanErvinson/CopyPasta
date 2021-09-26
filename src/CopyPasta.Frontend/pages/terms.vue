<template>
	<c-box d="flex" flexDirection="column" mx="auto" mb="2" align-items="center" justifyContent="center" h="100%" p="4">
		<c-text fontSize="lg" textAlign="center"
			>Anything posted on this site does not reflect the views and belief of the site owner.
		</c-text>
		<br />
		<c-text fontSize="lg"> Please don't sue me😂. </c-text>
	</c-box>
</template>

<script lang="ts">
import Vue from 'vue'
import { CText, CBox } from '@chakra-ui/vue'

export default Vue.extend({
	components: { CText, CBox },
})
</script>
