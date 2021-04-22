<template>
  <div class="vault">
    <div class="card" style="width: 18rem;" @click="getKeepsByVaultId(vault.id)">
      <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
        <div class="card-body">
          <h5 class="card-title">
            {{ vault.name }}
          </h5>
        </div>
      </router-link>
    </div>
  </div>
  <!-- MODAL -->
  <div class="modal fade"
       id="bear2"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            {{ state.activeVault.name }}
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body card-columns">
          <keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal" @click="deleteVault(state.activeVault.id)">
            Delete
          </button>
          <button class="btn btn-secondary dropdown-toggle"
                  type="button"
                  id="dropdownMenuButton1"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
          >
            Delete Keep
          </button>
          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
            <div class="dropdown-item text-primary"
                 href="#"
                 v-for="keeps in state.keeps"
                 :key="keeps.id"
                 :keeps="keeps"
                 @click="deleteVaultKeep(keeps.vaultKeepId)"
            >
              {{ keeps.name }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { Vault } from '../models/Vault'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { reactive, computed } from 'vue'

export default {
  name: 'Vault',
  props: {
    vault: { type: Object, default: () => new Vault() }
  },
  setup(props) {
    const state = reactive({
      activeVault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      vaultkeeps: computed(() => AppState.vaultkeeps)
    })
    return {
      getVaultById() {
        AppState.activeVault = {}
        vaultsService.getKeepById(props.vault.id)
      },
      getKeepsByVaultId(id) {
        vaultsService.getVaultById(id)
        vaultsService.getKeepsByVaultId(id)
      },
      deleteVaultKeep(id) {
        vaultsService.deleteVaultKeep(id)
      },
      deleteVault(id) {
        vaultsService.deleteVault(id)
        vaultsService.getVaultsByAccount()
      },
      state
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.card-columns {
  //small
  @media(max-width: 767px) {
    column-count: 2;
  }
  @media(min-width: 768px) {
    column-count: 3;
  }
  @media(min-width: 1200px) {
    column-count: 4;
  }
}
</style>
