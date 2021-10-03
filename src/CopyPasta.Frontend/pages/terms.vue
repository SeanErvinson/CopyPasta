<template>
	<c-box d="flex" flex-direction="column" mx="auto" mb="2" align-items="center" justify-content="center" h="100%" p="4">
		<c-text font-size="lg" text-align="center"
			>Anything posted on this site does not reflect the views and belief of the site owner.
		</c-text>
		<br />
		<c-text font-size="lg"> Please don't sue me😂. </c-text>
	</c-box>
</template>

<script lang="ts">
import Vue from 'vue'
import { CText, CBox } from '@chakra-ui/vue'

export default Vue.extend({
	components: { CText, CBox },
})
</script>
