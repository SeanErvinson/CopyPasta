declare module 'axios' {
	export interface AxiosError<T = never> extends Error {
		config: AxiosRequestConfig
		code?: string
		request?: any
		response?: AxiosResponse<T>
		isAxiosError: boolean
		toJSON: () => object
	}
	export interface AxiosResponse<T = never> {
		data: T
		status: number
		statusText: string
		headers: Record<string, string>
		request?: any
	}
}
