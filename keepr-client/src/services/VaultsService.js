import { AppState } from '../AppState'
// import router from '../router'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Vault } from '../models/Vault'
import { logger } from '../utils/Logger'
import { Profile } from '../models/Profile'
import { Keep } from '../models/Keep'

class VaultsService {
  async getVaults() {
    const res = await api.get('api/vaults')
    AppState.vaults = res.data.map(d => new Vault(d))
  }

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}/yeah`)
    AppState.activeVault = new Vault(res.data)
    logger.log(res.status)
  }

  async createVault(vaultData) {
    try {
      await api.post('api/vaults', vaultData)
      this.getVaults()
      this.getVaultsByAccount()
    } catch (error) {
      logger.error(error)
      this.getVaults()
    }
  }

  async deleteVault(id) {
    try {
      await api.delete('api/vaults/' + `${id}`)
      this.getVaultsByAccount()
      this.getVaults()
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultsByProfileId(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/vaults')
      AppState.vaults = res.data.map(d => new Vault(d))
      logger.log(res.data)
    } catch (error) {

    }
  }

  async getProfileById(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.activeProfile = new Profile(res.data)
  }

  async getKeepsByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    AppState.pkeeps = res.data.map(k => new Keep(k))
    logger.log(res.data)
  }

  async getVaultsByAccount() {
    const res = await api.get('account/vaults')
    AppState.userVaults = res.data.map(u => new Vault(u))
  }

  async createVaultKeep(newVault) {
    const res = await api.post('api/vaultkeeps', newVault)
    logger.log(res.data)
    return res
  }

  async getKeepsByVaultId(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    AppState.keeps = res.data.map(d => new Keep(d))
    logger.log(res.data)
    return res
  }

  async deleteVaultKeep(id, vaultId) {
    const res = await api.delete('api/vaultkeeps/' + id)
    this.getKeepsByVaultId(vaultId)
    return res
  }
}
export const vaultsService = new VaultsService()
