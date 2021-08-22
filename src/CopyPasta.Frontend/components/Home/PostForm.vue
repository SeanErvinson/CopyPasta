<template>
	<form class="px-5 flex-grow md:flex lg:flex md:gap-x-4 lg:gap-x-5">
		<div class="block mb-2 md:w-2/3 lg:w-3/4">
			<client-only>
				<TextArea v-model="form.content"></TextArea>
			</client-only>
		</div>
		<div class="block md:flex md:flex-col md:w-1/3 lg:w-1/4">
			<div class="block bg-white rounded-lg border border-grey-300 py-4 px-5 mb-2">
				<h2 class="font-semibold text-grey-700 text-lg">Options</h2>
				<label class="flex flex-col py-1.5" for="isCustomExpiration">
					<span>Expires in</span>
					<select v-if="!isCustomExpiration" v-model="form.expiration" class="form-select rounded-md">
						<option value="10">10 mins</option>
						<option value="30">30 mins</option>
						<option value="60">1 hour</option>
						<option selected>Never</option>
					</select>
					<input v-else type="datetime-local" class="form-input rounded-md" @change="calculatedExpiration" />
					<div>
						<input id="isCustomExpiration" v-model="isCustomExpiration" type="checkbox" />
						<span for="isCustomExpiration" class="select-none">Custom</span>
					</div>
				</label>
				<label class="flex flex-col py-1.5" for="isPasswordProtected">
					<div class="py-1">
						<input
							id="isPasswordProtected"
							v-model="isPasswordProtected"
							type="checkbox"
							@change="form.password = undefined"
						/>
						<span class="select-none">Password protected</span>
					</div>
					<input
						v-if="isPasswordProtected"
						v-model="form.password"
						type="text"
						class="form-input rounded-md block"
					/>
				</label>
				<label class="flex flex-col py-1.5" for="isCustomLink">
					<div class="py-1">
						<input
							id="isCustomLink"
							v-model="isCustomLink"
							type="checkbox"
							@change="form.customLink = undefined"
						/>
						<span class="select-none">Custom Link</span>
					</div>
					<input
						v-if="isCustomLink"
						v-model="form.customLink"
						type="text"
						class="form-input rounded-md block"
						@input="e => checkIfLinkExists(e.target.value)"
					/>
					<span v-if="isCustomLink" class="flex"
						>Your url will be {{ $config.axios.baseUrl }}/{{ form.customLink }}</span
					>
					<span v-if="linkExists && isCustomLink">Link has already been taken</span>
				</label>
			</div>
			<button
				class="w-full px-4 py-2 text-white font-semibold bg-blue-500 rounded"
				type="submit"
				@click.prevent="submitForm"
			>
				Create
			</button>
		</div>
	</form>
</template>

<script lang="ts">
import Vue from 'vue'
import debounce from 'debounce'
import { mapGetters } from 'vuex'
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
		...mapGetters('post', ['linkExists']),
	},
	async mounted() {
		await this.$store.dispatch('initialize')
	},
	methods: {
		async submitForm(): Promise<void> {
			await this.$store.dispatch('post/createPost', {
				content: this.form.content,
				password: this.form.password,
				customLink: this.form.customLink,
				expiration: this.form.expiration ?? this.calculatedExpiration,
			} as CreatePost)
		},
		checkIfLinkExists() {
			const store = this.$store
			debounce(async function (value: string) {
				await store.dispatch('post/checkIfLinkExists', value)
			}, 400)
		},
	},
})
</script>
