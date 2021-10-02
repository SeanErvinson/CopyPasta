<template>
	<c-flex d="flex" :flexDirection="['column', null, 'row']" mx="5" h="100%">
		<PasswordModal v-if="getInfo.isPasswordProtected" />
		<c-box bg="white" :flex="[1, null, 7, null]" borderRadius="md" :my="[2, null, 0]" :mr="[0, null, 2]">
			<client-only>
				<TextArea
					v-if="!isVisible"
					:content="postDetails.content"
					:isReadonly="true"
					@startup="setEditorContent"
				></TextArea>
			</client-only>
		</c-box>
		<c-box :flex="[null, null, 3, 2]" flexGrow="0" flexShrink="0" :my="[2, null, 0]" :ml="[0, null, 2]">
			<c-box bg="white" py="4" px="5" borderRadius="lg" borderColor="gray.300" :mb="3">
				<c-heading as="h2" size="lg" color="gray.700" fontWeight="semibold"> Info </c-heading>
				<c-box d="flex" :my="4" flexDirection="column" :rowGap="8">
					<c-box d="flex" justifyContent="space-between">
						<c-text color="gray.500" fontWeight="500">Created by</c-text>
						<c-text color="gray.500">{{
							postDetails.createdBy ? postDetails.createdBy : 'Anonymous '
						}}</c-text>
					</c-box>
					<c-box d="flex" justifyContent="space-between">
						<c-text color="gray.500" fontWeight="500">Created on</c-text>
						<c-text color="gray.500">{{ postDetails.createdOn }}</c-text>
					</c-box>
					<c-box v-if="postDetails.expiresIn" d="flex" justifyContent="space-between">
						<c-text color="gray.500" fontWeight="500">Expires on</c-text>
						<c-text color="gray.500">{{ postDetails.expiresIn }}</c-text>
					</c-box>
				</c-box>
			</c-box>
			<c-box d="flex" flexDirection="row" justifyContent="center" :columnGap="16">
				<c-icon-button
					variant-color="vue"
					aria-label="Copy to clipboard"
					size="lg"
					@click="copyContentToClipboard"
					icon="copy"
				/>
				<c-icon-button
					variant-color="vue"
					aria-label="Download"
					size="lg"
					@click="downloadToFile"
					icon="download"
				/>
				<c-popover use-portal>
					<c-popover-trigger>
						<c-icon-button variant-color="vue" aria-label="Share" size="lg" icon="share" />
					</c-popover-trigger>
					<c-popover-content z-index="4">
						<c-popover-arrow />
						<c-popover-body>
							<h3>Share</h3>
							<c-icon-button
								variant-color="blue"
								aria-label="Copy link to clipboard"
								size="lg"
								@click="copyUrlToClipboard"
								icon="link"
							/>
							<c-icon-button
								variant-color="blue"
								aria-label="Share to twitter"
								size="lg"
								icon="twitter"
							/>
						</c-popover-body>
					</c-popover-content>
				</c-popover>
			</c-box>
		</c-box>
		<c-box border-width="1px" rounded="lg" overflow="hidden" v-if="getError('postInfo')" bg="gray.200" p="8">
			<c-box mt="1" font-weight="semibold" as="h4" line-height="tight" is-truncated d="flex">
				<c-icon
					:name="isPostExpired ? 'info' : 'warning-alt'"
					size="24px"
					:color="isPostExpired ? 'blue.400' : 'red.400'"
					mr="2"
				/>
				<c-text>{{ getError('postInfo').reason }}</c-text>
			</c-box>
		</c-box>
	</c-flex>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters } from 'vuex'
import PasswordModal from '~/components/PasswordModal.vue'
import TextArea from '~/components/common/TextArea.vue'
import { copyToClipboard } from '~/utils/documentUtils'

export default Vue.extend({
	components: {
		PasswordModal,
		TextArea,
	},
	computed: {
		...mapGetters('post', ['getInfo', 'getPost', 'isVisible', 'resetPost']),
		...mapGetters('status', ['getError']),
		postDetails() {
			return {
				content: this.getPost.content,
				expiresIn: this.getPost.expiresIn ? new Date(this.getPost.expiresIn)?.toLocaleString() : null,
				createdOn: new Date(this.getPost.createdOn)?.toLocaleString(),
				createdBy: this.getPost.createdBy,
			}
		},
		isPostExpired(): boolean {
			return this.getError('postInfo').statusCode == 400
		},
	},
	async mounted() {
		await this.$store.dispatch('post/fetchPostInfo', this.$route.params.link)
	},
	async beforeDestroy() {
		await this.$store.dispatch('post/resetPost')
	},
	data() {
		return {
			content: '',
		}
	},
	methods: {
		setEditorContent(value: string) {
			this.content = value
		},
		copyUrlToClipboard() {
			copyToClipboard(window.location.href)
		},
		copyContentToClipboard() {
			copyToClipboard(this.content)
		},
		downloadToFile() {
			const contentBlob = new Blob([this.content], { type: 'text/plain' })
			var fileName = `${this.getInfo.id}.txt`
			const link = document.createElement('a')
			link.href = URL.createObjectURL(contentBlob)
			link.download = fileName
			link.click()
			URL.revokeObjectURL(link.href)
		},
	},
})
</script>

<style scoped>
.blurred {
	filter: blur(8px);
}
</style>
