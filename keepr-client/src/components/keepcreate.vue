<template>
  <div class="KeepCreate">
    <div class="modal fade"
         id="createModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Create Keep
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form class="form-inline" @submit.prevent="createKeep">
              <div class="form-group">
                <input type="text"
                       name="name"
                       id="name"
                       class="form-control"
                       placeholder="Enter keep Name"
                       aria-describedby="helpId"
                       v-model="state.newKeep.name"
                >
                <input type="text"
                       name="description"
                       id="description"
                       class="form-control"
                       placeholder="Enter Description of Keep"
                       aria-describedby="helpId"
                       v-model="state.newKeep.description"
                >
                <input type="text"
                       name="img"
                       id="img"
                       class="form-control"
                       placeholder="Enter Image of Keep"
                       aria-describedby="helpId"
                       v-model="state.newKeep.img"
                >
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
            <button class="btn btn-success" @click="createKeep">
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
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import $ from 'jquery'
export default {
  name: 'KeepCreate',
  setup() {
    const state = reactive({
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          state.newKeep = {}
          $('#createModal').modal('hide')
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
