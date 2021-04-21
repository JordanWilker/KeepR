export class PKeep {
  constructor(data = {}) {
    this.id = data.id || data._id || 'placeholder id'
    this.creatorId = data.creatorId || 'test creatorId'
    this.name = data.name || 'b'
    this.description = data.description
    this.img = data.img
    this.views = data.views
    this.shares = data.shares
    this.keeps = data.keeps
    this.vaultKeepId = data.vaultKeepId || 'c'
  }
}
