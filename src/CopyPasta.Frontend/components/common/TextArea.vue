<template>
	<editor-content :editor="editor" class="editor" />
</template>

<script lang="ts">
import Vue from 'vue'
import { Editor, EditorContent } from '@tiptap/vue-2'
import { Document } from '@tiptap/extension-document'
import { Paragraph } from '@tiptap/extension-paragraph'
import { Text } from '@tiptap/extension-text'

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
			default: undefined,
		},
	},
	data() {
		return {
			editor: null as unknown as Editor,
		}
	},
	mounted() {
		this.editor = new Editor({
			extensions: [Document, Paragraph, Text],
			autofocus: true,
			onUpdate: ({ editor }) => {
				let content = editor.getHTML()
				const json = editor.getJSON().content
				if (
					Array.isArray(json) &&
					json.length === 1 &&
					!Object.prototype.hasOwnProperty.call(json[0], 'content')
				) {
					content = ''
				}
				this.$emit('input', content)
			},
			editable: !this.isReadonly,
			parseOptions: {
				preserveWhitespace: true,
			},
		})
		this.editor.commands.setContent(this.content, false, {
			preserveWhitespace: true,
		})
		this.$emit('startup', this.editor.state.doc.textBetween(0, this.editor.state.doc.nodeSize - 2, '\n'))
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
	overflow: auto;
	white-space: pre-line;
}
.ProseMirror:focus-within {
	border: 2px solid #4299e1;
}
</style>
