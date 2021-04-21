import { AppState } from '../AppState'
// import router from '../router'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Keep } from '../models/Keep'
import { logger } from '../utils/Logger'

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(d => new Keep(d))
    AppState.pkeeps = res.data.map(p => new Keep(p))
  }

  async getKeepById(id) {
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = new Keep(res.data)
    logger.log(res.data)
  }

  async createKeep(keepData) {
    try {
      await api.post('api/keeps', keepData)
      this.getKeeps()
      this.getKeepsByAccountId()
    } catch (error) {
      logger.error(error)
      this.getKeeps()
    }
  }

  async deleteKeep(id) {
    try {
      await api.delete('api/keeps/' + `${id}`)
      this.getKeeps()
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepsByAccountId() {
    try {
      const res = await api.get('account/keeps')
      AppState.userKeeps = res.data.map(d => new Keep(d))
      logger.log(res.data)
      return res
    } catch (error) {
      logger.log(error)
    }
  }
}
export const keepsService = new KeepsService()
