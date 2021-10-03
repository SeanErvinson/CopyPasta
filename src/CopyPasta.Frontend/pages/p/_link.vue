<template>
	<c-flex d="flex" :flex-direction="['column', null, 'row']" mx="5" h="100%">
		<PasswordModal v-if="getInfo.isPasswordProtected" />
		<c-box bg="white" :flex="[1, null, 7, null]" border-radius="md" :my="[2, null, 0]" :mr="[0, null, 2]">
			<client-only>
				<TextArea
					v-if="!isVisible"
					:content="postDetails.content"
					:is-readonly="true"
					@startup="setEditorContent"
				></TextArea>
			</client-only>
			<error-prompt />
		</c-box>
		<c-box :flex="[null, null, 3, 2]" flex-grow="0" flex-shrink="0" :my="[2, null, 0]" :ml="[0, null, 2]">
			<c-box bg="white" py="4" px="5" border-radius="lg" border-color="gray.300" :mb="3">
				<c-heading as="h2" size="lg" color="gray.700" font-weight="semibold"> Info </c-heading>
				<c-box d="flex" :my="4" flex-direction="column" :row-gap="8">
					<c-box d="flex" justify-content="space-between">
						<c-text color="gray.500" font-weight="500">Created by</c-text>
						<c-text color="gray.500">{{ postDetails.createdBy }}</c-text>
					</c-box>
					<c-box d="flex" justify-content="space-between">
						<c-text color="gray.500" font-weight="500">Created on</c-text>
						<c-text color="gray.500">{{ postDetails.createdOn }}</c-text>
					</c-box>
					<c-box v-if="postDetails.expiresIn" d="flex" justify-content="space-between">
						<c-text color="gray.500" font-weight="500">Expires on</c-text>
						<c-text color="gray.500">{{ postDetails.expiresIn }}</c-text>
					</c-box>
				</c-box>
			</c-box>
			<action-bar :content="content" :post-link="postLink" />
		</c-box>
	</c-flex>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters } from 'vuex'
import PasswordModal from '~/components/PasswordModal.vue'
import TextArea from '~/components/common/TextArea.vue'
import { copyToClipboard } from '~/utils/documentUtils'
import ErrorPrompt from '~/components/Post/ErrorPrompt.vue'
import ActionBar from '~/components/Post/ActionBar.vue'

type PostDetail = {
	content: string
	expiresIn?: string
	createdOn: string
	createdBy: string
}

export default Vue.extend({
	components: {
		PasswordModal,
		TextArea,
		ActionBar,
		ErrorPrompt,
	},
	data() {
		return {
			content: '',
		}
	},
	computed: {
		...mapGetters('post', ['getInfo', 'getPost', 'isVisible']),
		postDetails(): PostDetail {
			return {
				content: this.getPost.content,
				expiresIn: this.getPost.expiresIn ? new Date(this.getPost.expiresIn)?.toLocaleString() : undefined,
				createdOn: this.getPost.createdOn ? new Date(this.getPost.createdOn)?.toLocaleString() : '---',
				createdBy: this.getPost.createdBy ? this.getPost.createdBy : 'Anonymous ',
			}
		},
		postLink(): String {
			return this.$route.params.link
		},
	},
	async mounted() {
		await this.$store.dispatch('post/fetchPostInfo', this.postLink)
	},
	async beforeDestroy() {
		await this.$store.dispatch('post/resetPost')
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
			const fileName = `${this.getInfo.id}.txt`
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
