<template>
	<editor-content :editor="editor" class="editor" />
</template>

<script lang="ts">
import Vue from 'vue'
import { Editor, EditorContent } from '@tiptap/vue-2'
import Document from '@tiptap/extension-document'
import Paragraph from '@tiptap/extension-paragraph'
import Text from '@tiptap/extension-text'
import Placeholder from '@tiptap/extension-placeholder'

export default Vue.extend({
	components: {
		EditorContent,
	},
	props: {
		isReadonly: {
			type: Boolean,
			default: false,
		},
		content: {
			type: String || undefined,
			required: false,
		},
	},
	data() {
		return {
			editor: null as unknown as Editor,
		}
	},
	mounted() {
		this.editor = new Editor({
			extensions: [Document, Paragraph, Text, Placeholder],
			autofocus: true,
			onUpdate: ({ editor }) => {
				let content = editor.getHTML(),
					json = editor.getJSON().content
				if (Array.isArray(json) && json.length === 1 && !json[0].hasOwnProperty('content')) {
					content = ''
				}
				this.$emit('input', content)
			},
			editable: !this.isReadonly,
			parseOptions: {
				preserveWhitespace: true,
			},
			content: this.content,
		})
		this.$emit('startup', this.editor.state.doc.textContent)
		this.editor.chain().focus().run()
	},
	beforeDestroy() {
		this.editor.destroy()
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
