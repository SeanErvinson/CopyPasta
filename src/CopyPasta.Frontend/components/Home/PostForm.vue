<template>
	<form class="md:flex">
		<div class="block md:flex-1 mx-auto px-5">
			<TextArea v-model="form.content"></TextArea>
		</div>
		<div class="block md:flex-initial">
			<h2>Options</h2>
			<label class="block py-1.5" for="isCustomExpiration">
				<span>Expires in</span>
				<select
					v-if="!isCustomExpiration"
					id="isCustomExpiration"
					v-model="form.expiration"
					class="form-select"
				>
					<option value="10">10 mins</option>
					<option value="30">30 mins</option>
					<option value="60">1 hour</option>
					<option selected>Never</option>
				</select>
				<input v-else type="datetime-local" class="form-input" @change="calculatedExpiration" />
				<span for="isCustomExpiration">Custom</span>
				<input v-model="isCustomExpiration" type="checkbox" />
			</label>
			<label class="block py-1.5" for="isPasswordProtected">
				<input
					id="isPasswordProtected"
					v-model="isPasswordProtected"
					type="checkbox"
					@change="form.password = undefined"
				/>
				<span>Password protected</span>
				<input v-if="isPasswordProtected" v-model="form.password" type="text" class="form-input" />
			</label>
			<label class="block py-1.5" for="isCustomLink">
				<input id="isCustomLink" v-model="isCustomLink" type="checkbox" @change="form.customLink = undefined" />
				<span>Custom Link</span>
				<input v-if="isCustomLink" v-model="form.customLink" type="text" class="form-input" />
				<span v-if="isCustomLink">Your url will be {{ $config.axios.baseUrl }}/{{ form.customLink }}</span>
			</label>
		</div>
		<button type="submit" @click.prevent="submitForm">Create</button>
	</form>
</template>

<script lang="ts">
import Vue from 'vue'
import TextArea from '../../components/common/TextArea.vue'
import { CreatePost } from '~/store/post'

interface Form {
	content: string
	expiration: number | undefined
	password: string | undefined
	customLink?: string | undefined
}

export default Vue.extend({
	components: {
		TextArea,
	},
	data() {
		return {
			isCustomExpiration: false,
			isPasswordProtected: false,
			isCustomLink: false,
			form: {
				content: '',
				expiration: undefined,
				password: undefined,
				customLink: undefined,
			} as Form,
		}
	},
	computed: {
		calculatedExpiration(): number | undefined {
			return this.form.expiration
		},
	},
	async mounted() {
		await this.$store.dispatch('initialize')
	},
	methods: {
		submitForm(): void {
			this.$store.dispatch('post/createPost', {
				content: this.form.content,
				password: this.form.password,
				customLink: this.form.customLink,
				expiration: this.form.expiration ?? this.calculatedExpiration,
			} as CreatePost)
		},
	},
})
</script>
