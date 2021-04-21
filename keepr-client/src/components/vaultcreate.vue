<template>
  <div class="VaultCreate">
    <div class="modal fade"
         id="createModalVault"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Create Vault
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form class="form-inline" @submit.prevent="createVault">
              <div class="form-group">
                <input type="text"
                       name="name"
                       id="name"
                       class="form-control"
                       placeholder="Enter vault Name"
                       aria-describedby="helpId"
                       v-model="state.newVault.name"
                >
                <input type="text"
                       name="description"
                       id="description"
                       class="form-control"
                       placeholder="Enter Description of Vault"
                       aria-describedby="helpId"
                       v-model="state.newVault.description"
                >
                <input type="checkbox" v-model="state.newVault.isPrivate">
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
            <button class="btn btn-success" @click="createVault">
              Create
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import $ from 'jquery'
export default {
  name: 'VaultCreate',
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          state.newVault = {}
          $('#createModalVault').modal('hide')
        } catch (error) {
          logger.log(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
</style>
