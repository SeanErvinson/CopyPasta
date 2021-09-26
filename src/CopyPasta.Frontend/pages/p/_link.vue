<template>
	<div class="container mx-auto h-full">
		<div v-if="getInfo.isPasswordProtected"><PasswordModal /></div>
		<div v-if="!isLocked" class="block p-4 h-full bg-white border border-gray-200 rounded shadow-sm">
			<p v-html="getContent"></p>
		</div>
	</div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters } from 'vuex'
import PasswordModal from '../../components/PasswordModal.vue'

export default Vue.extend({
	components: {
		PasswordModal,
	},
	computed: {
		...mapGetters('post', ['getInfo', 'getContent', 'isLocked']),
	},
	async mounted() {
		await this.$store.dispatch('post/fetchPostInfo', this.$route.params.link)
	},
})
</script>
