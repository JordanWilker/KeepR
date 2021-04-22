<template>
  <div v-if="state.activeVault.isPrivate == true && state.activeVault.creatorId != state.account.id">
    You Cannot VieW this Page
  </div>
  <div class="vault container-fluid" v-else>
    <h5 class="modal-title row" id="exampleModalLabel">
      {{ state.activeVault.name }}
    </h5>
  </div>
  <div class="modal-body card-columns">
    <keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
  </div>
  <div class="modal-footer">
    <button type="button" class="btn btn-secondary" data-dismiss="modal" v-if="state.activeVault.creatorId == state.account.id" @click="deleteVault()">
      Delete
    </button>
    <button class="btn btn-secondary dropdown-toggle"
            type="button"
            id="dropdownMenuButton1"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
            v-if="state.activeVault.creatorId == state.account.id"
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
</template>

<script>
import { Vault } from '../models/Vault'
import { vaultsService } from '../services/VaultsService'
// import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'

import { reactive, computed, onMounted } from 'vue'
export default {
  name: 'Vault',
  props: {
    vault: { type: Object, default: () => new Vault() }
  },
  setup(props) {
    const route = useRoute()
    const state = reactive({
      activeVault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      vaultkeeps: computed(() => AppState.vaultkeeps),
      account: computed(() => AppState.account)
    })
    onMounted(() => vaultsService.getKeepsByVaultId(route.params.id))
    onMounted(() => vaultsService.getVaultById(route.params.id))

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
        if (window.confirm('Sure?')) {
          vaultsService.deleteVaultKeep(id, route.params.id)
        }
      },
      deleteVault() {
        if (window.confirm('Are You Sure')) {
          vaultsService.deleteVault(route.params.id)
          vaultsService.getVaultsByAccount()
        }
      },
      state
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
