<template>
	<c-modal
		:is-open="isModalOpen"
		:on-close="returnToHome"
		is-centered
		:close-on-overlay-click="false"
		:initial-focus-ref="getPasswordRef"
		:close-on-esc="false"
	>
		<c-modal-content ref="content" class="modal">
			<c-modal-header>Password protected</c-modal-header>
			<c-modal-close-button />
			<c-modal-body>
				<c-input
					ref="input"
					v-model="password"
					type="password"
					placeholder="Enter password"
					@keypress.enter="verifyPassword"
				/>
			</c-modal-body>
			<c-modal-footer>
				<c-button variant-color="red" mr="3" @click="returnToHome"> Cancel </c-button>
				<c-button variant-color="blue" :disabled="isLoading" @click="verifyPassword">{{
					!isLoading ? 'Enter' : 'Verifying'
				}}</c-button>
			</c-modal-footer>
		</c-modal-content>
		<c-modal-overlay />
	</c-modal>
</template>

<script lang="ts">
import { AxiosError } from 'axios'
import Vue from 'vue'
import { GetPost } from '../store/post'

export default Vue.extend({
	data() {
		return {
			password: '',
			isModalOpen: true,
			isLoading: false,
		}
	},
	methods: {
		async verifyPassword() {
			this.isLoading = true
			try {
				await this.$store.dispatch('post/fetchPost', {
					link: this.$route.params.link,
					password: this.password,
				} as GetPost)
				this.isModalOpen = false
			} catch (err) {
				const error = err as unknown as AxiosError
				if (error.response?.status === 400) {
					this.$toast({
						title: 'Password is incorrect',
						description: 'The password you entered is incorrect.',
						status: 'warning',
						duration: 2000,
					})
				}
			} finally {
				this.isLoading = false
			}
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
