<template>
	<c-modal :is-open="isModalOpen" :on-close="resetPostModal" is-centered @close="resetPostModal">
		<c-modal-content ref="content" class="modal">
			<c-modal-header>Post</c-modal-header>
			<c-modal-close-button />
			<c-modal-body>
				<c-input-group>
					<c-input id="post-link" type="text" readonly :value="postLink" />
					<c-input-right-element>
						<c-icon-button
							icon="copy"
							aria-label="Copy to clipboard"
							style="cursor: pointer"
							@click="copyUrlToClipboard"
						/>
					</c-input-right-element>
				</c-input-group>
			</c-modal-body>
			<c-modal-footer>
				<c-button variant-color="blue" mr="3" @click="copyUrlToClipboard"> {{ copyText }} </c-button>
				<c-button @click="goToPost">Go to post</c-button>
			</c-modal-footer>
		</c-modal-content>
		<c-modal-overlay />
	</c-modal>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapActions } from 'vuex'
import { copyToClipboard } from '~/utils/documentUtils'

export default Vue.extend({
	data() {
		return {
			baseUrl: '',
			copyText: 'Copy',
		}
	},
	computed: {
		...mapGetters('post', ['isModalOpen', 'createdPost']),
		...mapGetters('status', ['getError']),
		postError(): boolean {
			return this.getError('post')
		},
		postLink(): string {
			return `${this.baseUrl}p/${this.createdPost.link}`
		},
	},
	watch: {
		postError(newState) {
			if (!newState) return
			this.$toast({
				title: 'An error occured',
				description: 'Something went wrong. Please try again.',
				status: 'error',
				duration: 2000,
			})
		},
	},
	mounted() {
		this.baseUrl = window.location.href
	},
	methods: {
		...mapActions('post', ['resetPostModal']),
		copyUrlToClipboard(): void {
			const link = document.querySelector('#post-link') as HTMLInputElement
			copyToClipboard(link.value)
			this.copyText = 'Copied'
			setInterval(() => {
				this.copyText = 'Copy'
			}, 1500)
		},
		goToPost(): void {
			this.$router.push(`p/${this.createdPost.link}`)
			this.$store.dispatch('post/resetPostModal')
		},
	},
})
</script>
