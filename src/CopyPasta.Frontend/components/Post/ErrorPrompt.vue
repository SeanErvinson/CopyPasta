<template>
	<c-alert v-if="getError('postInfo')" :status="isPostExpired ? 'warning' : 'error'">
		<c-alert-icon />
		{{ getError('postInfo').reason }}
	</c-alert>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters } from 'vuex'

export default Vue.extend({
	computed: {
		...mapGetters('status', ['getError']),
		isPostExpired(): boolean {
			return this.getError('postInfo').statusCode === 400 || this.getError('postInfo').statusCode === 404
		},
	},
})
</script>

<style></style>
