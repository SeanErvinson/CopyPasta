<template>
	<div class="flex justify-center h-screen blur items-center bg-gray-200 antialiased">
		<div
			class="flex flex-col w-2/3 sm:w-2/3 lg:w-1/3 max-w-2xl mx-auto rounded-lg border border-gray-300 shadow-xl"
		>
			<div
				class="flex flex-row justify-between p-6 bg-white border-b border-gray-200 rounded-tl-lg rounded-tr-lg"
			>
				<p class="font-semibold text-gray-700">Password protected</p>
				<svg
					class="w-6 h-6"
					fill="none"
					stroke="currentColor"
					viewBox="0 0 24 24"
					xmlns="http://www.w3.org/2000/svg"
				>
					<path
						stroke-linecap="round"
						stroke-linejoin="round"
						stroke-width="2"
						d="M6 18L18 6M6 6l12 12"
					></path>
				</svg>
			</div>
			<div class="flex flex-col px-6 py-5 bg-gray-50">
				<input
					v-model="password"
					type="password"
					placeholder="Enter password"
					class="p-5 bg-white border border-gray-200 rounded shadow-sm"
				/>
			</div>
			<div
				class="
					flex flex-row
					items-center
					justify-between
					p-5
					bg-white
					border-t border-gray-200
					rounded-bl-lg rounded-br-lg
				"
			>
				<button class="px-4 py-2 text-gray-600 font-semibold">Cancel</button>
				<button class="px-4 py-2 text-white font-semibold bg-blue-500 rounded" @click="verifyPassword">
					Enter
				</button>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
import Vue from 'vue'
import { GetPost } from '../store/post'

export default Vue.extend({
	data() {
		return {
			password: undefined,
		}
	},
	methods: {
		async verifyPassword() {
			await this.$store.dispatch('post/fetchPost', {
				link: this.$route.params.link,
				password: this.password,
			} as GetPost)
		},
	},
})
</script>
