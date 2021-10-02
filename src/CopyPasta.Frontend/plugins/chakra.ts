import Vue from 'vue'
import Chakra from '@chakra-ui/vue'
import CustomIcons from '~/ui/customIcons'

Vue.use(Chakra, {
	icons: {
		extend: {
			...CustomIcons,
		},
	},
})
