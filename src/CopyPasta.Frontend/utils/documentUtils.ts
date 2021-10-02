export const copyToClipboard = (value: string) => {
	const element = document.createElement('textarea')
	element.value = value
	element.setAttribute('readonly', '')
	element.style.position = 'absolute'
	element.style.left = '-9999px'
	document.body.appendChild(element)
	const selectedElement = document.getSelection()
	if (!selectedElement) return
	const selected = selectedElement.rangeCount > 0 ? selectedElement.getRangeAt(0) : false
	element.select()
	document.execCommand('copy')
	document.body.removeChild(element)
	if (selected) {
		selectedElement.removeAllRanges()
		selectedElement.addRange(selected)
	}
}
