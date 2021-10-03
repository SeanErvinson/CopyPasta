<template>
	<c-flex direction="column">
		<c-box flex="1">
			<PostForm />
		</c-box>
		<span v-if="getLoading('initial')">Loading</span>
		<span v-else>CopyPasta counter: {{ getStats.postCount }}</span>
		<CreatedPostModal />
	</c-flex>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters } from 'vuex'
import { CFlex, CBox } from '@chakra-ui/vue'
import PostForm from '~/components/Home/PostForm.vue'
import CreatedPostModal from '~/components/Home/CreatedPostModal.vue'

export default Vue.extend({
	components: {
		CFlex,
		CBox,
		PostForm,
		CreatedPostModal,
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
