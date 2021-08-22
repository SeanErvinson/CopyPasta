<template>
	<editor-content
		:editor="editor"
		class="w-full h-full p-4 resize-none border rounded-md focus:outline-none focus:ring focus:border-blue-300"
		@input="$emit('input', $event.target.value)"
	/>
</template>

<script lang="ts">
import Vue from 'vue'
import { Editor, EditorContent } from '@tiptap/vue-2'
import Document from '@tiptap/extension-document'
import Paragraph from '@tiptap/extension-paragraph'
import Text from '@tiptap/extension-text'
import CodeBlock from '@tiptap/extension-code-block'
import Placeholder from '@tiptap/extension-placeholder'

export default Vue.extend({
	components: {
		EditorContent,
	},
	data() {
		return {
			value: '',
			editor: null,
		}
	},
	mounted() {
		this.editor = new Editor({
			extensions: [Document, Paragraph, Text, CodeBlock, Placeholder],
			autofocus: true,
		})
		this.editor.chain().focus().toggleCodeBlock().run()
	},
	beforeDestroy() {
		;(this.editor as Editor).destroy()
	},
})
</script>

<style scoped>
.ProseMirror {
	height: 100%;
}
</style>
