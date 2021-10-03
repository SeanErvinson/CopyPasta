import * as vuex from 'vuex'

interface Error {
	statusCode: number
	reason?: string
}
interface Status {
	loading: boolean
	error?: Error
}
interface StatusMap {
	[key: string]: Status
}

export interface FlagStatus {
	name: string
	flag: boolean
}
export interface ErrorPayload {
	name: string
	error: Error
}

export const state = () => ({
	status: {
		initial: {
			loading: false,
			error: undefined,
		},
		post: {
			loading: false,
			error: undefined,
		},
		postInfo: {
			loading: false,
			error: undefined,
		},
		checkLinkExists: {
			loading: false,
			error: undefined,
		},
	} as StatusMap,
})

export type RootState = ReturnType<typeof state>

export const getters: vuex.GetterTree<RootState, RootState> = {
	getLoading: state => (type: string) => {
		return state.status[type].loading
	},
	getError: state => (type: string) => {
		return state.status[type].error
	},
}

export const mutations: vuex.MutationTree<RootState> = {
	SET_LOADING(state, status: FlagStatus) {
		state.status[status.name].loading = status.flag
	},
	SET_ERROR(state, payload: ErrorPayload) {
		state.status[payload.name].error = payload.error
	},
	RESET_STATE(state, name: string) {
		state.status[name].loading = false
		state.status[name].error = undefined
	},
}
