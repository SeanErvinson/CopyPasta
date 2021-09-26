const breakpoints = ['30em', '48em', '62em', '80em']

breakpoints.sm = breakpoints[0]
breakpoints.md = breakpoints[1]
breakpoints.lg = breakpoints[2]
breakpoints.xl = breakpoints[3]

export default {
	breakpoints: breakpoints,
	fonts: {
		heading: '"Avenir Next", sans-serif',
		body: 'system-ui, sans-serif',
		mono: 'Menlo, monospace',
	},
	colors: {
		brand: {
			50: '#daffff',
			100: '#b1fbfb',
			200: '#85f7f7',
			300: '#58f3f3',
			400: '#31f0f0',
			500: '#1ed7d7',
			600: '#0ca7a7',
			700: '#007777',
			800: '#004949',
			900: '#001a1a',
		},
	},
}
