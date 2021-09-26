<template>
	<c-modal :is-open="isModalOpen" :on-close="resetPostModal" is-centered @close="resetPostModal">
		<c-modal-content ref="content" class="modal">
			<c-modal-header>Post</c-modal-header>
			<c-modal-close-button />
			<c-modal-body>
				<c-input-group>
					<c-input type="text" id="post-link" readonly :value="postLink" />
					<c-input-right-element>
						<c-icon-button
							icon="sun"
							aria-label="Copy to clipboard"
							style="cursor: pointer"
							@click="copyToClipboard"
						/>
					</c-input-right-element>
				</c-input-group>
				{{ createdPost.expiration }}
			</c-modal-body>
			<c-modal-footer>
				<c-button variant-color="blue" mr="3" @click="copyToClipboard"> {{ copyText }} </c-button>
				<c-button @click="goToPost">Go to post</c-button>
			</c-modal-footer>
		</c-modal-content>
		<c-modal-overlay />
	</c-modal>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapActions } from 'vuex'

export default Vue.extend({
	mounted() {
		this.baseUrl = window.location.href
	},
	data() {
		return {
			baseUrl: '',
			copyText: 'Copy',
		}
	},
	computed: {
		...mapGetters('post', ['isModalOpen', 'createdPost']),
		postLink(): string {
			return `${this.baseUrl}p/${this.createdPost.link}`
		},
	},
	methods: {
		...mapActions('post', ['resetPostModal']),
		copyToClipboard(): void {
			const link = document.querySelector('#post-link') as HTMLInputElement
			link.select()
			link.setSelectionRange(0, 99999)
			document.execCommand('copy')
			window.getSelection()?.removeAllRanges()
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
