<template>
	<div class="md:flex">
		<div class="block md:flex-1 mx-auto px-5">
			<TextArea></TextArea>
		</div>
		<div class="block md:flex-initial">
			<h2>Options</h2>
			<div>
				<label for="expiration">Expires in</label>
				<select v-if="!customExpiration" id="expiration" name="expiration">
					<option value="">10 mins</option>
					<option value="">30 mins</option>
					<option value="">1 hour</option>
					<option value="-1">Never</option>
				</select>
				<input v-else type="datetime" />
				<label for="customExpiration">Custom</label>
				<input id="customExpiration" v-model="customExpiration" type="checkbox" name="customerExpiration" />
			</div>
			<input id="custom" type="checkbox" name="expiration" />
			<input id="password" type="checkbox" name="expiration" />
		</div>
	</div>
</template>

<script lang="ts">
import Vue from 'vue'
import TextArea from '../components/common/TextArea.vue'
export default Vue.extend({
	components: {
		TextArea,
	},
	data() {
		return {
			customExpiration: false,
		}
	},
})
</script>
