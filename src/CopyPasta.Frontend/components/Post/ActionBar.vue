<template>
	<c-box d="flex" flex-direction="row" justify-content="center" :column-gap="16">
		<c-tooltip has-arrow :label="copyText" placement="top" bg="black" :close-onlick="false">
			<c-icon-button
				variant-color="blue"
				aria-label="Download"
				size="lg"
				icon="copy"
				:disabled="isPostValid"
				@click="copyContentToClipboard"
			/>
		</c-tooltip>
		<c-tooltip has-arrow label="Download" placement="top" bg="black" :close-onlick="false">
			<c-icon-button
				variant-color="blue"
				aria-label="Download"
				size="lg"
				icon="download"
				:disabled="isPostValid"
				@click="downloadToFile"
			/>
		</c-tooltip>
		<c-popover use-portal>
			<c-popover-trigger>
				<c-icon-button variant-color="blue" aria-label="Share" size="lg" icon="share" :disabled="isPostValid" />
			</c-popover-trigger>
			<c-popover-content z-index="4">
				<c-popover-arrow />
				<c-popover-body>
					<h3>Share</h3>
					<c-icon-button
						variant-color="blue"
						aria-label="Copy link to clipboard"
						size="lg"
						icon="link"
						@click="copyUrlToClipboard"
					/>
					<c-icon-button
						variant-color="blue"
						aria-label="Share to twitter"
						size="lg"
						icon="twitter"
						@click="shareTwitter"
					/>
				</c-popover-body>
			</c-popover-content>
		</c-popover>
	</c-box>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters } from 'vuex'
import { copyToClipboard } from '~/utils/documentUtils'

export default Vue.extend({
	props: {
		content: {
			type: String,
			required: true,
		},
		postLink: {
			type: String,
			required: true,
		},
	},
	data() {
		return {
			copyText: 'Copy',
		}
	},
	computed: {
		...mapGetters('status', ['getError']),
		isPostValid(): boolean {
			return !!this.getError('postInfo')
		},
	},
	methods: {
		copyUrlToClipboard(): void {
			copyToClipboard(window.location.href)
			this.copyText = 'Copied'
			setInterval(() => {
				this.copyText = 'Copy'
			}, 1500)
		},
		copyContentToClipboard(): void {
			copyToClipboard(this.content)
			this.copyText = 'Copied'
			setInterval(() => {
				this.copyText = 'Copy'
			}, 1500)
		},
		shareTwitter(): void {
			const url = window.location.href
			window.open(
				'http://twitter.com/share?url=' + encodeURIComponent(url),
				'',
				'left=0,top=0,width=550,height=450,personalbar=0,toolbar=0,scrollbars=0,resizable=0',
			)
		},
		downloadToFile(): void {
			const contentBlob = new Blob([this.content], { type: 'text/plain' })
			const fileName = `${this.postLink}.txt`
			const link = document.createElement('a')
			link.href = URL.createObjectURL(contentBlob)
			link.download = fileName
			link.click()
			URL.revokeObjectURL(link.href)
		},
	},
})
</script>

<style></style>
