export interface ChakraToastOptions {
	position?: 'top' | 'right' | 'bottom' | 'left'
	duration?: number
	render?: (options: { onClose?: VoidFunction; id: any }) => any
	title?: string
	description?: string
	status?: 'info' | 'warning' | 'success' | 'error'
	variant?: 'solid' | 'subtle' | 'top-accent' | 'left-accent'
	isClosable?: boolean
}

declare module 'vue/types/vue' {
	export interface Vue {
		$toast: (options: ChakraToastOptions) => void
	}
}
