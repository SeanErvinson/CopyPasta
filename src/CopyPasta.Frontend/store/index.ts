import * as vuex from 'vuex'
import { FlagStatus } from './status'

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
		commit('status/SET_LOADING', { name: 'initial', flag: true } as FlagStatus)
		try {
			const stats = await this.$axios.$get('/statistics')
			commit('SET_STATS', stats)
		} catch (error) {
			commit('status/SET_ERROR', { name: 'initial', flag: true } as FlagStatus)
		}
		commit('status/SET_LOADING', { name: 'initial', flag: false } as FlagStatus)
	},
}
