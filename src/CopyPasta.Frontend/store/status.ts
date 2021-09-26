import * as vuex from 'vuex'

interface Status {
	loading: boolean
	error: boolean
}

interface StatusMap {
	[key: string]: Status
}

export interface FlagStatus {
	name: string
	flag: boolean
}

export const state = () => ({
	status: {
		initial: {
			loading: false,
			error: false,
		},
		post: {
			loading: false,
			error: false,
		},
		postInfo: {
			loading: false,
			error: false,
		},
		checkLinkExists: {
			loading: false,
			error: false,
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
	SET_ERROR(state, status: FlagStatus) {
		state.status[status.name].error = status.flag
	},
	RESET_STATE(state, name: string) {
		state.status[name].loading = false
		state.status[name].error = false
	},
}
