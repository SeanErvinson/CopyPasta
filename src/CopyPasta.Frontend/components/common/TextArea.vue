<template>
	<editor-content :editor="editor" class="editor" />
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
			editor: null as unknown as Editor,
		}
	},
	mounted() {
		this.editor = new Editor({
			extensions: [Document, Paragraph, Text, CodeBlock, Placeholder],
			autofocus: true,
			onUpdate: ({ editor }) => {
				let html = editor.getHTML()
				if (html === '<p></p>') html = ''
				this.$emit('input', html)
			},
		})
		this.editor.chain().focus().run()
	},
	beforeDestroy() {
		;(this.editor as Editor).destroy()
	},
})
</script>

<style scoped>
.editor {
	height: 100%;
	width: 100%;
}
</style>

<style>
.ProseMirror {
	height: 100%;
	width: 100%;
	padding: 16px;
	border: 1px solid #cbd5e0;
	border-radius: 0.375rem;
	outline: none;
	word-break: break-all;
}
.ProseMirror:focus-within {
	border: 2px solid #4299e1;
}
</style>
