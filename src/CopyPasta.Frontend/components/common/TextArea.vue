<template>
	<textarea
		:value="value"
		class="w-full resize-none border rounded-md"
		@input="$emit('input', $event.target.value)"
	></textarea>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
	data() {
		return {
			value: '',
		}
	},
})
</script>

<style scoped></style>
