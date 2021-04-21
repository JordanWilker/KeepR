<template>
  <div class="card bg-dark text-white">
    <img class="card-img" :src="keep.img" alt="Card image">
    <div class="card-img-overlay" data-toggle="modal" data-target="#bear" @click="getKeepById">
      <h3 class="card-text" style="position:absolute; bottom:0;">
        {{ keep.name }}
      </h3>
      <p>
        <router-link :to="{ name: 'Profile',params: { id: keep.creatorId } }" class="nav-link" data-toggle="modal" data-target="#bear">
          <i class="fa fa-user-circle-o" aria-hidden="true" style="position:absolute; bottom:0; right:0"></i>
        </router-link>
      </p>
    </div>
  </div>
  <!-- Modal -->
  <div class="modal fade"
       id="bear"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-xl container-fluid" role="document">
      <div class="modal-content">
        <div class="modal-body row">
          <div class="col-6">
            <img class="card-img" :src="state.activeKeep.img" alt="Card image">
          </div>
          <div class="col-6">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
            <div class="row">
              {{ state.activeKeep.keeps }}
            </div>
            <div class="row">
              {{ state.activeKeep.name }}
            </div>
            <div class="row">
              {{ state.activeKeep.description }}
            </div>
            <div class="row">
              <div class="dropdown">
                <button class="btn btn-secondary dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton"
                        data-toggle="dropdown"
                        aria-haspopup="true"
                        aria-expanded="false"
                >
                  Add To Vault
                </button>
                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                  <div class="dropdown-item text-primary"
                       href="#"
                       v-for="userVaults in state.userVaults"
                       :key="userVaults.id"
                       :user-vaults="userVaults"
                       @click="createVaultKeep(userVaults.id)"
                  >
                    {{ userVaults.name }}
                  </div>
                </div>
              </div>
              <i class="fa fa-trash-o" aria-hidden="true" v-if="state.activeKeep.creatorId == state.account.id" @click="deleteKeep"></i>
              <router-link :to="{ name: 'Profile', params: { id: state.activeKeep.creatorId } }" class="nav-link" data-dismiss="modal">
                <i class="fa fa-user-circle-o fa-2x" aria-hidden="true" style="position:absolute; bottom:0; right:0"></i>
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Keep } from '../models/Keep'
import { Vault } from '../models/Vault'
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'

export default {
  name: 'Keep',
  props: {
    keep: {
      type: Object,
      default: () => new Keep()
    },
    userVault: {
      type: Object,
      default: () => new Vault()
    }
  },
  setup(props) {
    const state = reactive({
      account: computed(() => AppState.account),
      keep: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),
      userVaults: computed(() => AppState.userVaults),
      newVault: {}
    })
    return {
      state,
      deleteKeep() {
        if (window.confirm('Are You sure')) {
          keepsService.deleteKeep(state.activeKeep.id)
          $('#bear').modal('hide')
        }
      },
      getKeepById() {
        AppState.activeKeep = {}
        keepsService.getKeepById(props.keep.id)
      },
      log() {
        logger.log('hi')
      },
      createVaultKeep(vaultId) {
        state.activeKeep.keeps++
        state.newVault.keepId = state.activeKeep.id
        state.newVault.vaultId = vaultId
        vaultsService.createVaultKeep(state.newVault)
        state.newVault = {}
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
