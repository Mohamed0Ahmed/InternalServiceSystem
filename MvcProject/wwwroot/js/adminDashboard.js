$(document).ready(function () {
    // Load Owners on page load
    loadOwners();

    // Create Store
    $('#createStoreForm').on('submit', function (e) {
        e.preventDefault();
        const storeName = $('#storeName').val();
        $.ajax({
            url: '/Admin/CreateStore',
            method: 'POST',
            data: { storeName },
            success: function (response) {
                if (response.success) {
                    $('#createStoreModal').modal('hide');
                    $('#storesTable tbody').append(`
                        <tr data-store-id="${response.storeId}">
                            <td>${response.storeId}</td>
                            <td>${response.storeName}</td>
                            <td>
                                <button type="button" class="btn btn-info btn-sm view-branches-btn" data-store-id="${response.storeId}">View Branches</button>
                                <button type="button" class="btn btn-warning btn-sm edit-store-btn" data-store-id="${response.storeId}" data-store-name="${response.storeName}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-store-btn" data-store-id="${response.storeId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    $('#storeName').val('');
                } else {
                    alert('Failed to create store.');
                }
            }
        });
    });

    // Edit Store
    $(document).on('click', '.edit-store-btn', function () {
        const storeId = $(this).data('store-id');
        const storeName = $(this).data('store-name');
        $('#editStoreId').val(storeId);
        $('#editStoreName').val(storeName);
        $('#editStoreModal').modal('show');
    });

    $('#editStoreForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editStoreId').val();
        const storeName = $('#editStoreName').val();
        $.ajax({
            url: '/Admin/EditStore',
            method: 'POST',
            data: { id, storeName },
            success: function (response) {
                if (response.success) {
                    $('#editStoreModal').modal('hide');
                    $(`#storesTable tr[data-store-id="${id}"] td:nth-child(2)`).text(storeName);
                    $(`#storesTable tr[data-store-id="${id}"] .edit-store-btn`).data('store-name', storeName);
                } else {
                    alert('Failed to update store.');
                }
            }
        });
    });

    // Delete Store
    $(document).on('click', '.delete-store-btn', function () {
        if (!confirm('Are you sure you want to delete this store?')) return;
        const storeId = $(this).data('store-id');
        $.ajax({
            url: '/Admin/DeleteStore',
            method: 'POST',
            data: { id: storeId },
            success: function (response) {
                if (response.success) {
                    $(`#storesTable tr[data-store-id="${storeId}"]`).remove();
                } else {
                    alert('Failed to delete store.');
                }
            }
        });
    });

    // View Branches
    $(document).on('click', '.view-branches-btn', function () {
        const storeId = $(this).data('store-id');
        $('#createBranchStoreId').val(storeId);
        $('#createGuestStoreId').val(storeId);
        loadBranches(storeId);
        $('#branchesModal').modal('show');
    });

    // Load Branches
    function loadBranches(storeId) {
        $.ajax({
            url: '/Admin/GetBranches',
            method: 'GET',
            data: { storeId },
            success: function (branches) {
                $('#branchesTable tbody').empty();
                branches.forEach(branch => {
                    $('#branchesTable tbody').append(`
                        <tr data-branch-id="${branch.id}">
                            <td>${branch.id}</td>
                            <td>${branch.branchName}</td>
                            <td>
                                <button type="button" class="btn btn-info btn-sm view-rooms-btn" data-branch-id="${branch.id}">View Rooms</button>
                                <button type="button" class="btn btn-warning btn-sm edit-branch-btn" data-branch-id="${branch.id}" data-branch-name="${branch.branchName}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-branch-btn" data-branch-id="${branch.id}">Delete</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Create Branch
    $('#createBranchForm').on('submit', function (e) {
        e.preventDefault();
        const storeId = $('#createBranchStoreId').val();
        const branchName = $('#branchName').val();
        $.ajax({
            url: '/Admin/CreateBranch',
            method: 'POST',
            data: { storeId, branchName },
            success: function (response) {
                if (response.success) {
                    $('#createBranchModal').modal('hide');
                    $('#branchesTable tbody').append(`
                        <tr data-branch-id="${response.branchId}">
                            <td>${response.branchId}</td>
                            <td>${response.branchName}</td>
                            <td>
                                <button type="button" class="btn btn-info btn-sm view-rooms-btn" data-branch-id="${response.branchId}">View Rooms</button>
                                <button type="button" class="btn btn-warning btn-sm edit-branch-btn" data-branch-id="${response.branchId}" data-branch-name="${response.branchName}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-branch-btn" data-branch-id="${response.branchId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    $('#branchName').val('');
                } else {
                    alert('Failed to create branch.');
                }
            }
        });
    });

    // Edit Branch
    $(document).on('click', '.edit-branch-btn', function () {
        const branchId = $(this).data('branch-id');
        const branchName = $(this).data('branch-name');
        $('#editBranchId').val(branchId);
        $('#editBranchName').val(branchName);
        $('#editBranchModal').modal('show');
    });

    $('#editBranchForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editBranchId').val();
        const branchName = $('#editBranchName').val();
        $.ajax({
            url: '/Admin/EditBranch',
            method: 'POST',
            data: { id, branchName },
            success: function (response) {
                if (response.success) {
                    $('#editBranchModal').modal('hide');
                    $(`#branchesTable tr[data-branch-id="${id}"] td:nth-child(2)`).text(branchName);
                    $(`#branchesTable tr[data-branch-id="${id}"] .edit-branch-btn`).data('branch-name', branchName);
                } else {
                    alert('Failed to update branch.');
                }
            }
        });
    });

    // Delete Branch
    $(document).on('click', '.delete-branch-btn', function () {
        if (!confirm('Are you sure you want to delete this branch?')) return;
        const branchId = $(this).data('branch-id');
        $.ajax({
            url: '/Admin/DeleteBranch',
            method: 'POST',
            data: { id: branchId },
            success: function (response) {
                if (response.success) {
                    $(`#branchesTable tr[data-branch-id="${branchId}"]`).remove();
                } else {
                    alert('Failed to delete branch.');
                }
            }
        });
    });

    // View Rooms
    $(document).on('click', '.view-rooms-btn', function () {
        const branchId = $(this).data('branch-id');
        $('#createRoomBranchId').val(branchId);
        $('#createGuestBranchId').val(branchId);
        loadRooms(branchId);
        $('#roomsModal').modal('show');
    });

    // Load Rooms
    function loadRooms(branchId) {
        $.ajax({
            url: '/Admin/GetRooms',
            method: 'GET',
            data: { branchId },
            success: function (rooms) {
                $('#roomsTable tbody').empty();
                rooms.forEach(room => {
                    $('#roomsTable tbody').append(`
                        <tr data-room-id="${room.id}">
                            <td>${room.id}</td>
                            <td>${room.roomName}</td>
                            <td>
                                <button type="button" class="btn btn-info btn-sm view-guests-btn" data-room-id="${room.id}">View Guests</button>
                                <button type="button" class="btn btn-warning btn-sm edit-room-btn" data-room-id="${room.id}" data-room-name="${room.roomName}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-room-btn" data-room-id="${room.id}">Delete</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Create Room
    $('#createRoomForm').on('submit', function (e) {
        e.preventDefault();
        const branchId = $('#createRoomBranchId').val();
        const roomName = $('#roomName').val();
        $.ajax({
            url: '/Admin/CreateRoom',
            method: 'POST',
            data: { branchId, roomName },
            success: function (response) {
                if (response.success) {
                    $('#createRoomModal').modal('hide');
                    $('#roomsTable tbody').append(`
                        <tr data-room-id="${response.roomId}">
                            <td>${response.roomId}</td>
                            <td>${response.roomName}</td>
                            <td>
                                <button type="button" class="btn btn-info btn-sm view-guests-btn" data-room-id="${response.roomId}">View Guests</button>
                                <button type="button" class="btn btn-warning btn-sm edit-room-btn" data-room-id="${response.roomId}" data-room-name="${response.roomName}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-room-btn" data-room-id="${response.roomId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    $('#roomName').val('');
                } else {
                    alert('Failed to create room.');
                }
            }
        });
    });

    // Edit Room
    $(document).on('click', '.edit-room-btn', function () {
        const roomId = $(this).data('room-id');
        const roomName = $(this).data('room-name');
        $('#editRoomId').val(roomId);
        $('#editRoomName').val(roomName);
        $('#editRoomModal').modal('show');
    });

    $('#editRoomForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editRoomId').val();
        const roomName = $('#editRoomName').val();
        $.ajax({
            url: '/Admin/EditRoom',
            method: 'POST',
            data: { id, roomName },
            success: function (response) {
                if (response.success) {
                    $('#editRoomModal').modal('hide');
                    $(`#roomsTable tr[data-room-id="${id}"] td:nth-child(2)`).text(roomName);
                    $(`#roomsTable tr[data-room-id="${id}"] .edit-room-btn`).data('room-name', roomName);
                } else {
                    alert('Failed to update room.');
                }
            }
        });
    });

    // Delete Room
    $(document).on('click', '.delete-room-btn', function () {
        if (!confirm('Are you sure you want to delete this room?')) return;
        const roomId = $(this).data('room-id');
        $.ajax({
            url: '/Admin/DeleteRoom',
            method: 'POST',
            data: { id: roomId },
            success: function (response) {
                if (response.success) {
                    $(`#roomsTable tr[data-room-id="${roomId}"]`).remove();
                } else {
                    alert('Failed to delete room.');
                }
            }
        });
    });

    // View Guests
    $(document).on('click', '.view-guests-btn', function () {
        const roomId = $(this).data('room-id');
        $('#createGuestRoomId').val(roomId);
        loadGuests(roomId);
        $('#guestsModal').modal('show');
    });

    // Load Guests
    function loadGuests(roomId) {
        $.ajax({
            url: '/Admin/GetGuests',
            method: 'GET',
            data: { roomId },
            success: function (guests) {
                $('#guestsTable tbody').empty();
                guests.forEach(guest => {
                    $('#guestsTable tbody').append(`
                        <tr data-guest-id="${guest.id}">
                            <td>${guest.id}</td>
                            <td>${guest.username}</td>
                            <td>${guest.phoneNumber}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-guest-btn" data-guest-id="${guest.id}" data-guest-username="${guest.username}" data-guest-password="${guest.password}" data-guest-phone="${guest.phoneNumber}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-guest-btn" data-guest-id="${guest.id}">Delete</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Create Guest
    $('#createGuestForm').on('submit', function (e) {
        e.preventDefault();
        const storeId = $('#createGuestStoreId').val();
        const branchId = $('#createGuestBranchId').val();
        const roomId = $('#createGuestRoomId').val();
        const username = $('#guestUsername').val();
        const password = $('#guestPassword').val();
        const phoneNumber = $('#guestPhoneNumber').val();
        $.ajax({
            url: '/Admin/CreateGuest',
            method: 'POST',
            data: { storeId, branchId, roomId, username, password, phoneNumber },
            success: function (response) {
                if (response.success) {
                    $('#createGuestModal').modal('hide');
                    $('#guestsTable tbody').append(`
                        <tr data-guest-id="${response.guestId}">
                            <td>${response.guestId}</td>
                            <td>${response.username}</td>
                            <td>${response.phoneNumber}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-guest-btn" data-guest-id="${response.guestId}" data-guest-username="${response.username}" data-guest-password="${response.password}" data-guest-phone="${response.phoneNumber}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-guest-btn" data-guest-id="${response.guestId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    $('#guestUsername').val('');
                    $('#guestPassword').val('');
                    $('#guestPhoneNumber').val('');
                } else {
                    alert('Failed to create guest.');
                }
            }
        });
    });

    // Edit Guest
    $(document).on('click', '.edit-guest-btn', function () {
        const guestId = $(this).data('guest-id');
        const username = $(this).data('guest-username');
        const password = $(this).data('guest-password');
        const phoneNumber = $(this).data('guest-phone');
        $('#editGuestId').val(guestId);
        $('#editGuestUsername').val(username);
        $('#editGuestPassword').val(password);
        $('#editGuestPhoneNumber').val(phoneNumber);
        $('#editGuestModal').modal('show');
    });

    $('#editGuestForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editGuestId').val();
        const username = $('#editGuestUsername').val();
        const password = $('#editGuestPassword').val();
        const phoneNumber = $('#editGuestPhoneNumber').val();
        $.ajax({
            url: '/Admin/EditGuest',
            method: 'POST',
            data: { id, username, password, phoneNumber },
            success: function (response) {
                if (response.success) {
                    $('#editGuestModal').modal('hide');
                    const row = $(`#guestsTable tr[data-guest-id="${id}"]`);
                    row.find('td:nth-child(2)').text(username);
                    row.find('td:nth-child(3)').text(phoneNumber);
                    row.find('.edit-guest-btn')
                        .data('guest-username', username)
                        .data('guest-password', password)
                        .data('guest-phone', phoneNumber);
                } else {
                    alert('Failed to update guest.');
                }
            }
        });
    });

    // Delete Guest
    $(document).on('click', '.delete-guest-btn', function () {
        if (!confirm('Are you sure you want to delete this guest?')) return;
        const guestId = $(this).data('guest-id');
        $.ajax({
            url: '/Admin/DeleteGuest',
            method: 'POST',
            data: { id: guestId },
            success: function (response) {
                if (response.success) {
                    $(`#guestsTable tr[data-guest-id="${guestId}"]`).remove();
                } else {
                    alert('Failed to delete guest.');
                }
            }
        });
    });

    // Load Owners
    function loadOwners() {
        $.ajax({
            url: '/Admin/GetOwners',
            method: 'GET',
            success: function (owners) {
                $('#ownersTable tbody').empty();
                owners.forEach(owner => {
                    $('#ownersTable tbody').append(`
                        <tr data-owner-id="${owner.id}">
                            <td>${owner.id}</td>
                            <td>${owner.username}</td>
                            <td>${owner.email}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-owner-btn" data-owner-id="${owner.id}" data-owner-username="${owner.username}" data-owner-email="${owner.email}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-owner-btn" data-owner-id="${owner.id}">Delete</button>
                            </td>
                        </tr>
                    `);
                });
            }
        });
    }

    // Create Owner
    $('#createOwnerForm').on('submit', function (e) {
        e.preventDefault();
        const username = $('#ownerUsername').val();
        const password = $('#ownerPassword').val();
        const email = $('#ownerEmail').val();
        $.ajax({
            url: '/Admin/CreateOwner',
            method: 'POST',
            data: { username, password, email },
            success: function (response) {
                if (response.success) {
                    $('#createOwnerModal').modal('hide');
                    $('#ownersTable tbody').append(`
                        <tr data-owner-id="${response.ownerId}">
                            <td>${response.ownerId}</td>
                            <td>${response.username}</td>
                            <td>${response.email}</td>
                            <td>
                                <button type="button" class="btn btn-warning btn-sm edit-owner-btn" data-owner-id="${response.ownerId}" data-owner-username="${response.username}" data-owner-email="${response.email}">Edit</button>
                                <button type="button" class="btn btn-danger btn-sm delete-owner-btn" data-owner-id="${response.ownerId}">Delete</button>
                            </td>
                        </tr>
                    `);
                    $('#ownerUsername').val('');
                    $('#ownerPassword').val('');
                    $('#ownerEmail').val('');
                } else {
                    alert('Failed to create owner: ' + response.message);
                }
            }
        });
    });

    // Edit Owner
    $(document).on('click', '.edit-owner-btn', function () {
        const ownerId = $(this).data('owner-id');
        const username = $(this).data('owner-username');
        const email = $(this).data('owner-email');
        $('#editOwnerId').val(ownerId);
        $('#editOwnerUsername').val(username);
        $('#editOwnerEmail').val(email);
        $('#editOwnerModal').modal('show');
    });

    $('#editOwnerForm').on('submit', function (e) {
        e.preventDefault();
        const id = $('#editOwnerId').val();
        const username = $('#editOwnerUsername').val();
        const email = $('#editOwnerEmail').val();
        $.ajax({
            url: '/Admin/EditOwner',
            method: 'POST',
            data: { id, username, email },
            success: function (response) {
                if (response.success) {
                    $('#editOwnerModal').modal('hide');
                    const row = $(`#ownersTable tr[data-owner-id="${id}"]`);
                    row.find('td:nth-child(2)').text(username);
                    row.find('td:nth-child(3)').text(email);
                    row.find('.edit-owner-btn')
                        .data('owner-username', username)
                        .data('owner-email', email);
                } else {
                    alert('Failed to update owner: ' + response.message);
                }
            }
        });
    });

    // Delete Owner
    $(document).on('click', '.delete-owner-btn', function () {
        if (!confirm('Are you sure you want to delete this owner?')) return;
        const ownerId = $(this).data('owner-id');
        $.ajax({
            url: '/Admin/DeleteOwner',
            method: 'POST',
            data: { id: ownerId },
            success: function (response) {
                if (response.success) {
                    $(`#ownersTable tr[data-owner-id="${ownerId}"]`).remove();
                } else {
                    alert('Failed to delete owner: ' + response.message);
                }
            }
        });
    });
});