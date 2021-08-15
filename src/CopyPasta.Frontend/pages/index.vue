<template>
	<div>
		<PostForm />
		<span v-if="getLoading('initial')">Loading</span>
		<span v-else>CopyPasta counter: {{ getStats.postCount }}</span>
	</div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters } from 'vuex'
import PostForm from '../components/Home/PostForm.vue'

export default Vue.extend({
	components: {
		PostForm,
	},
	data() {
		return {
			postCount: Number,
		}
	},
	computed: {
		...mapGetters(['getStats']),
		...mapGetters('status', ['getLoading']),
	},
	async mounted() {
		await this.$store.dispatch('initialize')
	},
})
</script>
