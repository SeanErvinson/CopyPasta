<template>
	<c-modal
		:is-open="isModalOpen"
		:on-close="returnToHome"
		is-centered
		:closeOnOverlayClick="false"
		:initialFocusRef="getPasswordRef"
		:closeOnEsc="false"
	>
		<c-modal-content ref="content" class="modal">
			<c-modal-header>Password protected</c-modal-header>
			<c-modal-close-button />
			<c-modal-body>
				<c-input v-model="password" type="password" placeholder="Enter password" ref="input" />
			</c-modal-body>
			<c-modal-footer>
				<c-button variant-color="red" mr="3" @click="returnToHome"> Cancel </c-button>
				<c-button variant-color="blue" @click="verifyPassword">Enter</c-button>
			</c-modal-footer>
		</c-modal-content>
		<c-modal-overlay />
	</c-modal>
</template>

<script lang="ts">
import Vue from 'vue'
import { GetPost } from '../store/post'

export default Vue.extend({
	data() {
		return {
			password: '',
			isModalOpen: true,
		}
	},
	methods: {
		async verifyPassword() {
			await this.$store.dispatch('post/fetchPost', {
				link: this.$route.params.link,
				password: this.password,
			} as GetPost)
			this.isModalOpen = false
		},
		returnToHome() {
			this.$router.push('/')
		},
		getPasswordRef() {
			return this.$refs.input
		},
	},
})
</script>
