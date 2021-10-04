<template>
	<CFormControl d="flex" :flex-direction="['column', null, 'row']" mx="5" h="100%">
		<c-box bg="white" :flex="[1, null, 7, null]" border-radius="md" :my="[2, null, 0]" :mr="[0, null, 2]">
			<client-only>
				<TextArea v-model="form.content"></TextArea>
			</client-only>
		</c-box>
		<c-box :flex="[null, null, 3, 2]" flex-grow="0" flex-shrink="0" :my="[2, null, 0]" :ml="[0, null, 2]">
			<c-box bg="white" py="4" px="5" border-radius="lg" border-color="gray.300" :mb="3">
				<c-heading as="h2" size="lg" color="gray.700" font-weight="semibold"> Options </c-heading>
				<c-box as="label" d="flex" flex-direction="column" py="1" for="isCustomExpiration">
					<span>Expires in</span>
					<c-select v-if="!isCustomExpiration" v-model="form.expiration" border-radius="md">
						<option value="10">10 mins</option>
						<option value="30">30 mins</option>
						<option value="60">1 hour</option>
						<option :value="'' || undefined" selected>Never</option>
					</c-select>
					<c-input
						v-else
						type="datetime-local"
						:value="customExpirationDate"
						@change="calculatedExpiration"
					/>
					<div>
						<input
							id="isCustomExpiration"
							v-model="isCustomExpiration"
							type="checkbox"
							@change="form.expiration = undefined"
						/>
						<span for="isCustomExpiration">Custom</span>
					</div>
				</c-box>
				<c-box as="label" d="flex" flex-direction="column" py="1" for="isPasswordProtected">
					<c-box>
						<input
							id="isPasswordProtected"
							v-model="isPasswordProtected"
							type="checkbox"
							@change="form.password = ''"
						/>
						<span>Password protected</span>
					</c-box>

					<c-input-group v-if="isPasswordProtected">
						<c-input
							v-model="form.password"
							:type="showPassword ? 'text' : 'password'"
							placeholder="Enter password"
						/>
						<c-input-right-addon>
							<c-button
								h="1rem"
								size="sm"
								variant="link"
								outline="none"
								@click="showPassword = !showPassword"
							>
								{{ showPassword ? 'Hide' : 'Show' }}
							</c-button>
						</c-input-right-addon>
					</c-input-group>
				</c-box>
				<c-box as="label" d="flex" flex-direction="column" py="1" for="isCustomLink">
					<c-box>
						<input
							id="isCustomLink"
							v-model="isCustomLink"
							type="checkbox"
							@change="form.customLink = ''"
						/>
						<span>Custom Link</span>
					</c-box>
					<c-input
						v-if="isCustomLink"
						v-model="form.customLink"
						type="text"
						class="form-input rounded-md block"
						@input="checkIfLinkExists"
					/>
					<c-box v-if="linkExists && isCustomLink" d="block" as="span" w="100%"
						>Link has already been taken</c-box
					>
				</c-box>
			</c-box>
			<c-button
				w="100%"
				variant-color="blue"
				size="lg"
				type="submit"
				:disabled="getLoading('post') || !form.content"
				@click.prevent="submitForm"
			>
				{{ getLoading('post') ? 'Creating' : 'Create' }}
			</c-button>
		</c-box>
	</CFormControl>
</template>

<script lang="ts">
import Vue from 'vue'
import debounce from 'debounce'
import { mapGetters } from 'vuex'
import { CBox, CButton, CHeading, CFormControl, CSelect, CInput } from '@chakra-ui/vue'
import TextArea from '~/components/common/TextArea.vue'
import { CreatePost } from '~/store/post'

interface Form {
	content: string
	expiration: number | undefined
	password: string
	customLink: string
}

export default Vue.extend({
	components: {
		TextArea,
		CBox,
		CButton,
		CHeading,
		CInput,
		CSelect,
		CFormControl,
	},
	data() {
		return {
			isCustomExpiration: false,
			isPasswordProtected: false,
			isCustomLink: false,
			showPassword: false,
			form: {
				content: '',
				expiration: undefined,
				password: '',
				customLink: '',
			} as Form,
			customExpirationDate: '',
		}
	},
	computed: {
		...mapGetters('post', ['linkExists']),
		...mapGetters('status', ['getLoading']),
	},
	created() {
		this.checkIfLinkExists = debounce(this.checkIfLinkExists, 400)
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
				expiration: this.form.expiration,
			} as CreatePost)
		},
		calculatedExpiration(e: Event) {
			const input = e.target as HTMLInputElement
			const date = new Date(input.value)
			const delta = date.valueOf() - new Date().valueOf()
			this.form.expiration = delta / 60000
			this.customExpirationDate = input.value
		},
		async checkIfLinkExists(value: string) {
			if (!value) return
			await this.$store.dispatch('post/checkIfLinkExists', value)
		},
	},
})
</script>
