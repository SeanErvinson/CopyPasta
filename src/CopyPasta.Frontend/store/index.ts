import { AxiosError } from 'axios'
import * as vuex from 'vuex'
import { ErrorPayload, FlagStatus } from './status'

interface Stat {
	postCount: number | undefined
}

export const state = () => ({
	stats: { postCount: undefined } as Stat,
})

export type RootState = ReturnType<typeof state>

export const getters: vuex.GetterTree<RootState, RootState> = {
	getStats: state => {
		return state.stats
	},
}

export const mutations: vuex.MutationTree<RootState> = {
	SET_STATS(state, stats) {
		state.stats = stats
	},
}

export const actions: vuex.ActionTree<RootState, RootState> = {
	async initialize({ commit }) {
		commit('status/SET_LOADING', { name: 'initial', flag: true } as FlagStatus, { root: true })
		try {
			const stats = await this.$axios.$get('/statistics')
			commit('SET_STATS', stats)
		} catch (err: unknown) {
			const error = err as AxiosError<Error>
			commit(
				'status/SET_ERROR',
				{
					name: 'initial',
					error: { statusCode: error.response?.status, reason: error.response?.data.message },
				} as ErrorPayload,
				{ root: true },
			)
		}
		commit('status/SET_LOADING', { name: 'initial', flag: false } as FlagStatus, { root: true })
	},
}
