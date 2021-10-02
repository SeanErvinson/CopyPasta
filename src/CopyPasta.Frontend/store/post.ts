import { AxiosError } from 'axios'
import * as vuex from 'vuex'
import { Post } from '~/common/types'
import { ErrorPayload, FlagStatus } from './status'

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

interface CreatePostResponse {
	link: string
	expiration?: Date | undefined
}

export const state = () => ({
	info: {} as PostInfo,
	isVisible: true,
	post: {} as Post,
	linkExists: false,
	isModalOpen: false,
	createdPost: {} as CreatePostResponse,
})

export type RootState = ReturnType<typeof state>

export const getters: vuex.GetterTree<RootState, RootState> = {
	getInfo: state => {
		return state.info
	},
	isVisible: state => {
		return state.isVisible
	},
	getPost: state => {
		return state.post
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
	SET_IS_VISIBLE(state, flag: boolean) {
		state.isVisible = flag
	},
	SET_POST(state, post: Post) {
		state.post = post
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
	RESET_POST(state) {
		state.post = {} as Post
		state.info = {} as PostInfo
		state.isVisible = true
		state.linkExists = false
		state.isModalOpen = false
	},
}

export const actions: vuex.ActionTree<RootState, RootState> = {
	async fetchPostInfo({ commit, dispatch }, link: string) {
		commit('status/SET_LOADING', { name: 'postInfo', flag: true } as FlagStatus, { root: true })
		try {
			const postInfo = (await this.$axios.$get(`posts/${link}/info`)) as PostInfo
			commit('SET_INFO', postInfo)
			if (postInfo.isPasswordProtected) commit('SET_IS_VISIBLE', postInfo.isPasswordProtected)
			else dispatch('fetchPost', { link } as GetPost)
		} catch (err: unknown) {
			const error = err as AxiosError<Error>
			commit(
				'status/SET_ERROR',
				{
					name: 'postInfo',
					error: { statusCode: error.response?.status, reason: error.response?.data.message },
				} as ErrorPayload,
				{
					root: true,
				},
			)
		} finally {
			commit('status/SET_LOADING', { name: 'postInfo', flag: false } as FlagStatus, { root: true })
		}
	},
	async fetchPost({ commit }, payload: GetPost) {
		commit('status/SET_LOADING', { name: 'post', flag: true } as FlagStatus, { root: true })
		try {
			const post = (await this.$axios.$post(`posts/${payload.link}`, { password: payload.password })) as Post
			commit('SET_IS_VISIBLE', false)
			commit('SET_POST', post)
		} catch (err: unknown) {
			const error = err as AxiosError<Error>
			commit(
				'status/SET_ERROR',
				{
					name: 'post',
					error: { statusCode: error.response?.status, reason: error.response?.data.message },
				} as ErrorPayload,
				{ root: true },
			)
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
		} catch (err: unknown) {
			const error = err as AxiosError<Error>
			commit(
				'status/SET_ERROR',
				{
					name: 'post',
					error: { statusCode: error.response?.status, reason: error.response?.data.message },
				} as ErrorPayload,
				{ root: true },
			)
		} finally {
			commit('status/SET_LOADING', { name: 'post', flag: false } as FlagStatus, { root: true })
		}
	},
	async checkIfLinkExists({ commit }, value: string) {
		commit('status/SET_LOADING', { name: 'checkLinkExists', flag: true } as FlagStatus, { root: true })
		try {
			const result = await this.$axios.$get(`posts/${value}/exists`)
			commit('SET_LINK_EXIST', result)
		} catch (err: unknown) {
			const error = err as AxiosError<Error>
			commit(
				'status/SET_ERROR',
				{
					name: 'checkLinkExists',
					error: { statusCode: error.response?.status, reason: error.response?.data.message },
				} as ErrorPayload,
				{ root: true },
			)
		} finally {
			commit('status/SET_LOADING', { name: 'checkLinkExists', flag: false } as FlagStatus, { root: true })
		}
	},
	resetPost({ commit }) {
		commit('RESET_POST')
	},
	resetPostModal({ commit }) {
		commit('SET_IS_MODAL_OPEN', false)
		commit('SET_CREATED_POST', {})
	},
}
