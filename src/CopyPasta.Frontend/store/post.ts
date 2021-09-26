import * as vuex from 'vuex'
import { FlagStatus } from './status'

interface PostInfo {
	id: string
	isPasswordProtected: boolean
	expiresIn: Date
}
export interface CreatePost {
	content: string
	expiration: number | undefined
	password: string | undefined
	customLink: string | undefined
}

export interface GetPost {
	link: string
	password: string | undefined
}

interface Post {
	content: string
	expiresOn: Date
}

interface CreatePostResponse {
	link: string
	expiration?: Date | undefined
}

export const state = () => ({
	info: {} as PostInfo,
	isLocked: true,
	content: '',
	linkExists: false,
	isModalOpen: false,
	createdPost: {} as CreatePostResponse,
})

export type RootState = ReturnType<typeof state>

export const getters: vuex.GetterTree<RootState, RootState> = {
	getInfo: state => {
		return state.info
	},
	isLocked: state => {
		return state.isLocked
	},
	getContent: state => {
		return state.content
	},
	linkExists: state => {
		return state.linkExists
	},
	isModalOpen: state => {
		return state.isModalOpen
	},
	createdPost: state => {
		return state.createdPost
	},
}

export const mutations: vuex.MutationTree<RootState> = {
	SET_INFO(state, info: PostInfo) {
		state.info = info
	},
	SET_LOCK(state, flag: boolean) {
		state.isLocked = flag
	},
	SET_CONTENT(state, content: string) {
		state.content = content
	},
	SET_CREATED_POST(state, createdPost: CreatePostResponse) {
		state.createdPost = createdPost
	},
	SET_LINK_EXIST(state, flag: boolean) {
		state.linkExists = flag
	},
	SET_IS_MODAL_OPEN(state, flag: boolean) {
		state.isModalOpen = flag
	},
}

export const actions: vuex.ActionTree<RootState, RootState> = {
	async fetchPostInfo({ commit, dispatch }, link: string) {
		commit('status/SET_LOADING', { name: 'postInfo', flag: true } as FlagStatus, { root: true })
		try {
			const postInfo = (await this.$axios.$get(`posts/${link}/info`)) as PostInfo
			commit('SET_INFO', postInfo)
			if (postInfo.isPasswordProtected) commit('SET_LOCK', postInfo.isPasswordProtected)
			else dispatch('fetchPost', { link } as GetPost)
		} catch (error) {
			commit('status/SET_ERROR', { name: 'postInfo', flag: true } as FlagStatus, { root: true })
		}
		commit('status/SET_LOADING', { name: 'postInfo', flag: false } as FlagStatus, { root: true })
	},
	async fetchPost({ commit }, payload: GetPost) {
		commit('status/SET_LOADING', { name: 'post', flag: true } as FlagStatus, { root: true })
		try {
			const post = (await this.$axios.$post(`posts/${payload.link}`, { password: payload.password })) as Post
			commit('SET_LOCK', false)
			commit('SET_CONTENT', post.content)
		} catch (error) {
			commit('status/SET_ERROR', { name: 'post', flag: true } as FlagStatus, { root: true })
		} finally {
			commit('status/SET_LOADING', { name: 'post', flag: false } as FlagStatus, { root: true })
		}
	},
	async createPost({ commit }, payload: CreatePost) {
		commit('status/RESET_STATE', 'post', { root: true })
		commit('status/SET_LOADING', { name: 'post', flag: true } as FlagStatus, { root: true })
		try {
			const response = (await this.$axios.$post(`posts`, payload)) as CreatePostResponse
			commit('SET_IS_MODAL_OPEN', true)
			commit('SET_CREATED_POST', response)
		} catch (error) {
			commit('status/SET_ERROR', { name: 'post', flag: true } as FlagStatus, { root: true })
		} finally {
			commit('status/SET_LOADING', { name: 'post', flag: false } as FlagStatus, { root: true })
		}
	},
	async checkIfLinkExists({ commit }, value: string) {
		commit('status/SET_LOADING', { name: 'checkLinkExists', flag: true } as FlagStatus, { root: true })
		try {
			const result = await this.$axios.$get(`posts/${value}/exists`)
			commit('SET_LINK_EXIST', result)
		} catch (error) {
			commit('status/SET_ERROR', { name: 'checkLinkExists', flag: true } as FlagStatus, { root: true })
		}
		commit('status/SET_LOADING', { name: 'checkLinkExists', flag: false } as FlagStatus, { root: true })
	},
	resetPostModal({ commit }) {
		commit('SET_IS_MODAL_OPEN', false)
		commit('SET_CREATED_POST', {})
	},
}
